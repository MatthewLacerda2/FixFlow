using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using Server.Models;
using Server.Data;
using MongoDB.Driver;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Server;

public class Startup {
        
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {

        services.AddIdentityCore<Client>()
            .AddEntityFrameworkStores<ServerContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityCore<Employee>()
            .AddEntityFrameworkStores<ServerContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityCore<Secretary>()
            .AddEntityFrameworkStores<ServerContext>()
            .AddDefaultTokenProviders();

        // Configure DbContext for MySQL
        services.AddDbContext<ServerContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 23))));

        // Configure MongoDB
        string connectionString = Configuration.GetConnectionString("MongoDbConnection")!;
        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
        services.AddSingleton<IMongoDatabase>(provider =>
        {
            var client = provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase("mongo_db");
        });

        // Configure authentication
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.Cookie.Name = "Flow_Cookie";
            });

        services.AddAuthorization();

        services.AddControllersWithViews();

        services.AddRateLimiter(_ => _
            .AddFixedWindowLimiter(policyName: "fixed", options =>
            {
                options.PermitLimit = 10;
                options.Window = TimeSpan.FromSeconds(5);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 10;
            }));

        // Add Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlowAPI", Version = "v1" });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

        });

        // Add other services as needed
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flow API v1"));

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}