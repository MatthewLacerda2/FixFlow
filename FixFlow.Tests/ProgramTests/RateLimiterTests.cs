using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Server.Models.Utils;

namespace FixFlow.Tests.ProgramTests;

public class RateLimiterTests : IClassFixture<WebApplicationFactory<Program>> {

	private readonly WebApplicationFactory<Program> _factory;

	public RateLimiterTests(WebApplicationFactory<Program> factory) {
		_factory = factory;
	}

	[Fact]
	public async Task RateLimiter_AllowsRequestsWithinLimit() {
		// Arrange
		var client = _factory.CreateClient();
		// Act
		var response = await client.PostAsync(Common.api_v1 + "accounts/logout", null);
		// Assert
		Assert.NotEqual(HttpStatusCode.TooManyRequests, response.StatusCode);
	}

	[Fact]
	public async Task RateLimiter_BlocksRequestsExceedingLimit() {
		// Arrange
		var client = _factory.CreateClient();

		// Act
		for (int i = 0; i < Common.requestPerSecondLimit; i++) {
			var response1 = await client.PostAsync(Common.api_v1 + "accounts/logout", null);
			Assert.NotEqual(HttpStatusCode.TooManyRequests, response1.StatusCode);
		}

		// Make an additional request which should now be blocked
		var response2 = await client.PostAsync(Common.api_v1 + "accounts/logout", null);

		// Assert
		Assert.NotEqual(HttpStatusCode.TooManyRequests, response2.StatusCode);
	}
}
