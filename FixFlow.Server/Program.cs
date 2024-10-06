using System.Text;
using System.Threading.RateLimiting;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Data;
using Server.Models;
using Server.Models.Utils;
public class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);

		string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

		builder.Services.AddDbContext<ServerContext>(options =>
			options.UseNpgsql(connectionString));

		builder.Services.AddIdentity<Business, IdentityRole>()
			.AddEntityFrameworkStores<ServerContext>()
			.AddDefaultTokenProviders();

		builder.Services.AddIdentityCore<Customer>().AddEntityFrameworkStores<ServerContext>();

		builder.Services.AddCors(options => {
			options.AddPolicy("AllowAnyOrigin",
				builder => {
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
		});

		builder.Services.AddAuthorization();

		var secretKey = builder.Configuration["Jwt:SecretKey"];
		var verifiedIssuerSigningKey = secretKey != null ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
			: throw new InvalidOperationException("JWT secret key is missing in configuration.");

		builder.Services.AddAuthentication(options => {
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options => {
			options.TokenValidationParameters = new TokenValidationParameters {
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = builder.Configuration["Jwt:Issuer"],
				ValidAudience = builder.Configuration["Jwt:Audience"],
				IssuerSigningKey = verifiedIssuerSigningKey,
			};

			options.Events = new JwtBearerEvents {
				OnAuthenticationFailed = context => {
					var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
					logger.LogError("Token failed: {Exception}", context.Exception.Message);
					return Task.CompletedTask;
				}
			};
		});

		builder.Services.AddRateLimiter(rate => {
			rate.AddFixedWindowLimiter(policyName: "fixed", options => {
				options.PermitLimit = Common.requestPerSecondLimit;
				options.Window = TimeSpan.FromSeconds(1);
				options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
				options.QueueLimit = 0;
			})
			.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
		});

		builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

		builder.Services.AddControllersWithViews();

		// if (!args.Contains("swagger")) {
		// 	Serilog.Log.Logger = new LoggerConfiguration()
		// 	.Enrich.FromLogContext().
		// 	WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions {
		// 		AutoCreateSqlDatabase = true,
		// 		TableName = "Serilogs"
		// 	}).CreateLogger();
		// }

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(options => {
			options.SwaggerDoc("v1", new OpenApiInfo { Title = "Flow API", Version = "1.0" });

			var xmlFile = "FixFlow.Server.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			options.IncludeXmlComments(xmlPath);
		});

		var app = builder.Build();

		app.UseCors("AllowAnyOrigin");

		app.UseDefaultFiles();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.MapFallbackToFile("/index.html");

		app.Run();
	}
}
