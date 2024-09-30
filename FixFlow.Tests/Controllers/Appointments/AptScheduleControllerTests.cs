using Server.Controllers;
using Server.Data;

namespace FixFlow.Tests.Controllers;

public class AptScheduleControllerTests {

	private readonly ServerContext _context;
	private readonly AptScheduleController _controller;

	public AptScheduleControllerTests() {

		_context = new Util().SetupDbContextForTests();

		_controller = new AptScheduleController(_context);
	}

}
