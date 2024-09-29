using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace FixFlow.Tests;

public class Util {
	public ServerContext SetupDbContextForTests() {
		var connectionStringBuilder = new SqliteConnectionStringBuilder();
		connectionStringBuilder.DataSource = ":memory:";

		var connection = new SqliteConnection(connectionStringBuilder.ToString());

		DbContextOptions<ServerContext> _dbContextOptions = new DbContextOptionsBuilder<ServerContext>()
			.UseSqlite(connection)
			.Options;

		ServerContext _context = new ServerContext(_dbContextOptions);
		_context.Database.OpenConnection();
		_context.Database.EnsureCreated();

		return _context;
	}
}
