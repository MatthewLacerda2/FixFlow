using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using Server.Models;
using System.Text;
using System.Threading.RateLimiting;
using MongoDB.Driver;
using FluentValidation.AspNetCore;
using Server.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<Client, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ServerContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<ServerContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 23))));

string connectionString = builder.Configuration.GetConnectionString("MongoDbConnection")!;
builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
builder.Services.AddSingleton<IMongoDatabase>(provider =>
{
    var client = provider.GetRequiredService<IMongoClient>();
    return client.GetDatabase("mongo_db");
});

var secretKey = builder.Configuration["Jwt:SecretKey"];
var verifiedIssuerSigningKey = secretKey != null ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    : throw new InvalidOperationException("JWT secret key is missing in configuration.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = verifiedIssuerSigningKey
    };
})
.AddFacebook(options =>
{
    options.AppId = "client";
    options.AppSecret = "secret";
})
.AddGoogle(options =>
{
    options.ClientId = "client";
    options.ClientSecret = "secret";
})
.AddMicrosoftAccount(options =>
{
    options.ClientId = "";
    options.ClientSecret = "secret";
});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromSeconds(5);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 10;
    }));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAnyOrigin");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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