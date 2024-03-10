using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using Server.Models;
using Server.Data;
using MongoDB.Driver;

namespace Server;

public class Startup {
        
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {

        services.AddIdentity<Client, IdentityRole>()
            .AddEntityFrameworkStores<ServerContext>()
            .AddDefaultTokenProviders();

        services.AddIdentity<Employee, IdentityRole>()
            .AddEntityFrameworkStores<ServerContext>()
            .AddDefaultTokenProviders();

        services.AddIdentity<Secretary, IdentityRole>()
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
            return client.GetDatabase("YourMongoDatabaseName");
        });

        // Configure authentication
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.LoginPath = "/Accounts/Login";
                options.AccessDeniedPath = "/Accounts/Login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.Cookie.Name = "Flow_Cookie";
            });

        services.AddAuthorization();

        // Add Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlowAPI", Version = "v1" });
        });

        // Add other services as needed
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flow API v0.5"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseAuthentication();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
    }