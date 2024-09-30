using Microsoft.AspNetCore.Mvc;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Filters;

namespace FixFlow.Tests.Controllers;

public class AptScheduleControllerTests {

	private readonly ServerContext _context;
	private readonly AptScheduleController _controller;

	public AptScheduleControllerTests() {

		_context = new Util().SetupDbContextForTests();

		_controller = new AptScheduleController(_context);
	}

	[Fact]
	public async Task ReadSchedules_ReturnsFilteredSchedules() {
		// Arrange
		var client1Name = "fulano da silva";

		var business1 = new Business("b-1", "b-1@gmail.com", "789.4561.123-0001", "98999344788") {
			allowListedServicesOnly = true,
			services = ["Service 1", "Service 2"]
		};
		var business2 = new Business("b-2", "b-2@gmail.com", "123.4567.789-0001", "98988263255");
		var client1 = new Client(business1.Id, "789456123", client1Name, null, null, null);
		var client2 = new Client(business2.Id, "123456789", "ciclano da silva", null, null, null);
		var client3 = new Client(business1.Id, "123456789", "beltrano da silva", null, null, null);

		var schedules = new List<AptSchedule>();
		for (int i = 0; i < 11; i++) {
			schedules.Add(new AptSchedule(client1.Id, business1.Id, DateTime.Now.AddDays(i), i * 10, "Service 1"));
		}

		schedules[0].BusinessId = business2.Id;
		schedules[0].ClientId = client2.Id;
		schedules[1].ClientId = client3.Id;

		schedules[10] = schedules[6];
		schedules[10].Service = "Service 2";

		_context.Business.AddRange(business1, business2);
		_context.Clients.AddRange(client1, client2, client3);

		_context.Schedules.AddRange(schedules);
		_context.SaveChanges();

		var filter = new AptScheduleFilter {
			businessId = business1.Id,
			client = "fulano ",
			minPrice = 20,
			maxPrice = 90,
			minDateTime = DateTime.Now.AddDays(3),
			maxDateTime = DateTime.Now.AddDays(8),
			service = "Service 1",
			sort = ScheduleSort.Price,
			descending = true,
			offset = 1,
			limit = 1
		};

		var expectedSchedule = schedules[7];

		// Act
		var result = await _controller.ReadSchedules(filter) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(200, result!.StatusCode);
		var filteredLogs = result.Value as IEnumerable<AptSchedule>;
		Assert.Single(filteredLogs);

		Assert.Equal(client1Name, filteredLogs!.First().Client.FullName);
		Assert.Equal(expectedSchedule.Price, filteredLogs!.First().Price);
		Assert.Equal(DateTime.Now.AddDays(7).Date, filteredLogs!.First().dateTime.Date);
		Assert.Equal(expectedSchedule.Service, filteredLogs!.First().Service);
	}
}
