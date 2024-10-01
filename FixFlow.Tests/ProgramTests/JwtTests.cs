using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using Server.Models;
using Server.Models.Utils;

namespace FixFlow.Tests.ProgramTests;

public class JwtTests : IClassFixture<WebApplicationFactory<Program>> {
	private readonly WebApplicationFactory<Program> _factory;

	public JwtTests(WebApplicationFactory<Program> factory) {
		_factory = factory;
	}

	[Fact]
	public async Task ProtectedEndpoint_RequiresAuthentication() {
		// Arrange
		var client = _factory.CreateClient();
		// Act
		var response = await client.GetAsync(Common.api_v1 + nameof(Business));
		// Assert
		Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
	}

	[Fact]
	public async Task ProtectedEndpoint_ReturnsOk_WithValidToken() {
		// Arrange
		var client = _factory.CreateClient();
		var token = GenerateJwtToken();

		// Act
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		var response = await client.GetAsync(Common.api_v1 + nameof(Business));

		// Assert
		response.EnsureSuccessStatusCode();
		var responseBody = await response.Content.ReadAsStringAsync();
		Assert.Equal("You are authenticated!", responseBody);
	}

	private string GenerateJwtToken() {
		var key = Encoding.UTF32.GetBytes("xpvista7810");

		var claims = new List<Claim> {
			new Claim(ClaimTypes.Name, "Test Business"),
			new Claim(ClaimTypes.Email, "testbusiness@example.com"),
			new Claim("businessId", "test-business-id")
		};

		var tokenDescriptor = new SecurityTokenDescriptor {
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddMinutes(Common.tokenExpirationTimeInMinutes),
			Issuer = "Flow",
			Audience = "user",
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}
}
