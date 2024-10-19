using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Bogus;
using Server.Models;
using Server.Models.Appointments;
namespace Server.Data;

public class ServerContext : IdentityDbContext {

	public DbSet<Business> Business { get; set; } = default!;
	public DbSet<Customer> Customers { get; set; } = default!;

	public DbSet<IdlePeriod> IdlePeriods { get; set; } = default!;

	public DbSet<AptSchedule> Schedules { get; set; } = default!;
	public DbSet<AptLog> Logs { get; set; } = default!;
	public DbSet<AptContact> Contacts { get; set; } = default!;

	public ServerContext(DbContextOptions<ServerContext> options)
		: base(options) {
	}

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);

		DBSeeder dbSeeder = new DBSeeder(builder, 617);

		builder.Entity<Business>()
			   .HasIndex(b => b.CNPJ)
			   .IsUnique();

		builder.Entity<Customer>()
			.Property(c => c.PhoneNumber)
			.IsRequired();

		builder.Entity<IdlePeriod>()
				.HasIndex(c => c.Id)
				.IsUnique();

		builder.Entity<IdlePeriod>()
				.HasOne<Business>()
				.WithMany()
				.HasForeignKey(i => i.BusinessId);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		if (optionsBuilder.IsConfigured) {
			return;
		}
		base.OnConfiguring(optionsBuilder);

		optionsBuilder.UseNpgsql(
			"Host=localhost;port=3306;Database=fixflow;User=lendacerda;Password=xpvista7810;");
	}
}
