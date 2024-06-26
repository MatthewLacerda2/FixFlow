using Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Server.Models.Appointments;
using Server.Seeder;
using Server.Models.PasswordReset;
namespace Server.Data;

public class ServerContext : IdentityDbContext
{

    public DbSet<Client> Clients { get; set; } = default!;
    public DbSet<Business> Business { get; set; } = default!;

    public DbSet<AptLog> Logs { get; set; } = default!;
    public DbSet<AptContact> Contacts { get; set; } = default!;
    public DbSet<AptSchedule> Schedules { get; set; } = default!;

    public DbSet<PasswordReset> Resets { get; set; } = default!;

    public ServerContext(DbContextOptions<ServerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //FlowSeeder flowSeeder = new FlowSeeder(builder, 617);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(
            "Server=localhost;port=3306;Database=fixflow;User=lendacerda;Password=xpvista7810;",
            new MariaDbServerVersion(new Version(10, 5, 11)));
    }
}