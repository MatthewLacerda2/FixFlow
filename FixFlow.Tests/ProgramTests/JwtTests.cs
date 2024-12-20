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
		var response = await client.GetAsync($"{Common.api_v1}{nameof(Business)}?businessId=test-business-id");
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
		var response = await client.GetAsync(Common.api_v1 + nameof(Business) + "/Business?test-business-id");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	private string GenerateJwtToken() {

		var key = Encoding.UTF8.GetBytes("VeryLongSecretKey123456789012345678901234567890123456789012345678901234567890");

		var claims = new List<Claim> {
			new Claim(ClaimTypes.Name, "Test Business"),
			new Claim(ClaimTypes.Email, "testbusiness@example.com"),
			new Claim("businessId", "test-business-id")
		};

		var tokenDescriptor = new SecurityTokenDescriptor {
			Subject = new ClaimsIdentity(claims),
			Issuer = "Flow",
			Audience = "user",
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}
}
