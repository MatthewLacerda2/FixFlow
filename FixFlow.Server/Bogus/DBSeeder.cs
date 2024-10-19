using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Appointments;

namespace Server.Bogus;

//Ideally, the DB would seed automatically when its empty, and we'd have a command to seed the DB willy-nilly
public class DBSeeder {

	public Generator generator;

	public DBSeeder(ModelBuilder builder, int seed) {
		generator = new Generator(seed);

		var businesses = generator.GetFakeBusiness(10);

		var customers = new List<Customer>();
		for (int i = 0; i < businesses.Length; i++) {
			var newCustomers = generator.GetFakeCustomers(100, businesses[i].Id);
			customers.AddRange(newCustomers);
		}

		var schedules = new List<AptSchedule>();
		for (int i = 0; i < businesses.Length; i++) {
			for (int j = 0; j < customers.Count; j++) {
				var newSchedules = generator.GetFakeSchedules(1, businesses[i].Id, customers[i].Id);
				schedules.AddRange(newSchedules);
			}
		}

		var logs = new List<AptLog>();
		for (int i = 0; i < schedules.Count; i++) {
			var log = generator.GetFakeLogs(1, schedules[i].BusinessId, schedules[i].CustomerId);
			log[0].ScheduleId = schedules[i].Id;    //Only at Index 0 because there is only one AptLog being generated
			logs.AddRange(log);
		}

		DateTime feriasStart = new DateTime(2024, 7, 14, 1, 0, 0);
		DateTime feriasFinish = new DateTime(2024, 7, 30, 1, 0, 0);

		var idlePeriods = new List<IdlePeriod>();
		for (int i = 0; i < businesses.Length; i++) {
			IdlePeriod idlePeriod = new IdlePeriod(businesses[i].Id, feriasStart, feriasFinish, "Recesso de Julho");
			idlePeriods.Add(idlePeriod);
		}

		builder.Entity<Business>().HasData(businesses);
		builder.Entity<Customer>().HasData(customers);

		builder.Entity<IdlePeriod>().HasData(idlePeriods);

		builder.Entity<AptSchedule>().HasData(schedules);
		builder.Entity<AptLog>().HasData(logs);
	}
}
