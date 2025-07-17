using Microsoft.AspNetCore.Mvc;
using TomskPlaces.Infrastructure.Persistence; // или твой namespace

namespace TomskPlaces.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DbTestController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public DbTestController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> TestConnection()
	{
		try
		{
			var canConnect = await _context.Database.CanConnectAsync();
			return Ok(canConnect ? "✅ Connected to DB" : "❌ Failed to connect");
		}
		catch (Exception ex)
		{
			return BadRequest($"❌ Error: {ex.Message}");
		}
	}
}
