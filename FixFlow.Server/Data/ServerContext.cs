using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Appointments;
namespace Server.Data;

public class ServerContext : IdentityDbContext {

	public DbSet<Client> Clients { get; set; } = default!;
	public DbSet<Business> Business { get; set; } = default!;

	public DbSet<AptLog> Logs { get; set; } = default!;
	public DbSet<AptContact> Contacts { get; set; } = default!;
	public DbSet<AptSchedule> Schedules { get; set; } = default!;

	public DbSet<IdlePeriod> IdlePeriods { get; set; } = default!;

	public DbSet<OTP> OTPs { get; set; } = default!;

	public ServerContext(DbContextOptions<ServerContext> options)
		: base(options) {
	}

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);

		//FlowSeeder flowSeeder = new FlowSeeder(builder, 617);

		builder.Entity<Business>()
			   .HasIndex(b => b.CNPJ)
			   .IsUnique();

		builder.Entity<Client>()
			.Property(c => c.PhoneNumber)
			.IsRequired();

		builder.Entity<IdlePeriod>()
				.HasIndex(c => c.Id)
				.IsUnique();

		builder.Entity<IdlePeriod>()
				.HasOne<Business>()
				.WithMany()
				.HasForeignKey(i => i.BusinessId);

		builder.Entity<Business>()
		   .OwnsMany(b => b.BusinessDays);
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
