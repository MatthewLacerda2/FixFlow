using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Server.Models;
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
		HttpContent content = new StringContent("\"98999344788\"", Encoding.UTF8, "application/json");

		// Act
		var response1 = await client.PostAsync(Common.api_v1 + nameof(OTP), content);

		// Assert
		Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
	}

	[Fact]
	public async Task RateLimiter_BlocksRequestsExceedingLimit() {
		// Arrange
		var client = _factory.CreateClient();
		HttpContent content = new StringContent("\"98999344788\"", Encoding.UTF8, "application/json");

		// Act
		for (int i = 0; i < Common.requestPerSecondLimit; i++) {
			var response = await client.PostAsync(Common.api_v1 + nameof(OTP), content);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		var blockedResponse = await client.GetAsync(Common.api_v1 + nameof(OTP));

		// Assert
		Assert.Equal(HttpStatusCode.TooManyRequests, blockedResponse.StatusCode);
	}
}
