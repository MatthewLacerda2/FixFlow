using Microsoft.AspNetCore.Mvc.Testing;

namespace FixFlow.Tests.ProgramTests;

public class ProgramTests : IClassFixture<WebApplicationFactory<Program>> {

	private readonly WebApplicationFactory<Program> _factory;

	public ProgramTests(WebApplicationFactory<Program> factory) {
		_factory = factory;
	}

	[Fact]
	public async Task GetSwaggerUI_ReturnsOk() {
		// Arrange
		var client = _factory.CreateClient();

		// Act
		var response = await client.GetAsync("/swagger");

		// Assert
		response.EnsureSuccessStatusCode(); // Status Code 200-299
		var responseString = await response.Content.ReadAsStringAsync();
		Assert.Contains("Swagger", responseString);
	}
}
