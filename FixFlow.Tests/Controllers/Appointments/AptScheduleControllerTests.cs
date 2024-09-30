using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.DTO;
using Server.Models.Erros;
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

	[Fact]
	public async Task CreateSchedule_ReturnsBadRequest_WhenClientDoesNotExist() {
		// Arrange
		var newAppointment = new CreateAptSchedule("non-exist-client", DateTime.Now.AddDays(1), 100, "Service 1", null);

		// Act
		var result = await _controller.CreateSchedule(newAppointment) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(400, result!.StatusCode);
		Assert.Equal(NotExistErrors.Client, result.Value);
	}

	[Fact]
	public async Task CreateSchedule_ReturnsBadRequest_WhenServiceIsUnlisted() {
		// Arrange
		var business = new Business("b-1", "b-1@gmail.com", "789.4561.123-0001", "98999344788") {
			allowListedServicesOnly = true,
			services = ["Service 1", "Service 2"]
		};
		var client = new Client(business.Id, "789456123", "fulano da silva", null, null, null);

		_context.Business.Add(business);
		_context.Clients.Add(client);
		_context.SaveChanges();

		var newAppointment = new CreateAptSchedule(client.Id, DateTime.Now.AddDays(1), 100, "Service 3", null);

		// Act
		var result = await _controller.CreateSchedule(newAppointment) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(400, result!.StatusCode);
		Assert.Equal(ValidatorErrors.UnlistedService, result.Value);
	}

	[Fact]
	public async Task CreateSchedule_ReturnsBadRequest_WhenTimeNotWithinBusinessHours() {
		// Arrange
		var business = new Business("b-1", "b-1@gmail.com", "789.4561.123-0001", "98999344788") {
			allowListedServicesOnly = false,
		};
		var client = new Client(business.Id, "789456123", "fulano da silva", null, null, null);

		_context.Business.Add(business);
		_context.Clients.Add(client);
		_context.SaveChanges();

		DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now);
		var dateTime = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day, 6, 0, 0);

		var newAppointment = new CreateAptSchedule(client.Id, dateTime, 100, null, null);

		// Act
		var result = await _controller.CreateSchedule(newAppointment) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(400, result!.StatusCode);
		Assert.Equal(ValidatorErrors.TimeNotWithinBusinessHours, result.Value);
	}
	/*
		[Fact]
		public async Task CreateSchedule_ReturnsBadRequest_WhenDateWithinIdlePeriod() {
			// Arrange
			var business = new Business("b-1", "b-1@gmail.com", "789.4561.123-0001", "98999344788") {
				allowListedServicesOnly = true,
				services = ["Service 1", "Service 2"],
			};
			var client = new Client(business.Id, "789456123", "fulano da silva", null, null, null);

			_context.Business.Add(business);
			_context.Clients.Add(client);
			_context.IdlePeriods.Add(new IdlePeriod {
				BusinessId = business.Id,
				start = DateTime.Today.AddHours(10),
				finish = DateTime.Today.AddHours(12)
			});
			_context.SaveChanges();

			var newAppointment = new AptSchedule {
				ClientId = client.Id,
				BusinessId = business.Id,
				dateTime = DateTime.Today.AddHours(11), // Within idle period
				Price = 100,
				Service = "Service 1"
			};

			// Act
			var result = await _controller.CreateSchedule(newAppointment) as BadRequestObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(400, result!.StatusCode);
			Assert.Equal(ValidatorErrors.DateWithinIdlePeriod, result.Value);
		}

		[Fact]
		public async Task CreateSchedule_ReturnsCreated_WhenValidRequest() {
			// Arrange
			var business = new Business("b-1", "b-1@gmail.com", "789.4561.123-0001", "98999344788") {
				allowListedServicesOnly = true,
				services = ["Service 1", "Service 2"],
				BusinessDays = new List<BusinessDay> {
					new BusinessDay {
						Start = DateTime.Today.AddHours(9),
						Finish = DateTime.Today.AddHours(17)
					}
				}
			};
			var client = new Client(business.Id, "789456123", "fulano da silva", null, null, null);

			_context.Business.Add(business);
			_context.Clients.Add(client);
			_context.SaveChanges();

			var newAppointment = new AptSchedule {
				ClientId = client.Id,
				BusinessId = business.Id,
				dateTime = DateTime.Today.AddHours(10),
				Price = 100,
				Service = "Service 1"
			};

			// Act
			var result = await _controller.CreateSchedule(newAppointment) as CreatedAtActionResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(201, result!.StatusCode);
			var createdSchedule = result.Value as AptSchedule;
			Assert.NotNull(createdSchedule);
			Assert.Equal(newAppointment.ClientId, createdSchedule!.ClientId);
			Assert.Equal(newAppointment.BusinessId, createdSchedule.BusinessId);
			Assert.Equal(newAppointment.dateTime, createdSchedule.dateTime);
			Assert.Equal(newAppointment.Price, createdSchedule.Price);
			Assert.Equal(newAppointment.Service, createdSchedule.Service);
		}
	*/
}
