using Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Server.Models.Appointments;
namespace Server.Data;

public class ServerContext : IdentityDbContext
{

    public ServerContext(DbContextOptions<ServerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employee>().HasQueryFilter(x => x.isDeleted == false); //Only Read those NOT DELETED
        builder.Entity<Client>().HasQueryFilter(x => x.isDeleted == false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(
            "Server=localhost;port=3306;Database=mysql;User=lendacerda;Password=xpvista7810;",
            new MariaDbServerVersion(new Version(10, 5, 11)));
    }

    public DbSet<Client> Clients { get; set; } = default!;
    public DbSet<Employee> Employees { get; set; } = default!;

    public DbSet<AptLog> Logs { get; set; } = default!;
    public DbSet<AptReminder> Reminders { get; set; } = default!;
    public DbSet<AptSchedule> Schedules { get; set; } = default!;
}