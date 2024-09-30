using Microsoft.AspNetCore.Identity;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;

namespace FixFlow.Tests.Controllers;

public class AptLogControllerTests {

	private readonly ServerContext _context;
	private readonly AptLogController _controller;
	private readonly Mock<UserManager<Business>> _mockUserManager;
	private readonly Mock<UserManager<Client>> _mockClientManager;

	public AptLogControllerTests() {

		_context = new Util().SetupDbContextForTests();

		var store = new Mock<IUserStore<Business>>();
		_mockUserManager = new Mock<UserManager<Business>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		var clientStore = new Mock<IUserStore<Client>>();
		_mockClientManager = new Mock<UserManager<Client>>(clientStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		_controller = new AptLogController(_context, _mockUserManager.Object, _mockClientManager.Object);
	}

}
