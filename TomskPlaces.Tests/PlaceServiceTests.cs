using Microsoft.EntityFrameworkCore;
using TomskPlaces.Domain.Entities;
using TomskPlaces.Domain.Enums;
using TomskPlaces.Infrastructure.Persistence;
using TomskPlaces.Infrastructure.Services;
using Xunit;

namespace TomskPlaces.Tests;

public class PlaceServiceTests
{
	private ApplicationDbContext GetInMemoryDbContext()
	{
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase("TestDb")
			.Options;

		return new ApplicationDbContext(options);
	}

	[Fact]
	public async Task GetPlaceAsync_ReturnsCorrectPlace()
	{
		var context = GetInMemoryDbContext();
		var place = new Catering { Name = "Test Place", Type = 0 };
		context.Places.Add(place);
		await context.SaveChangesAsync();

		var service = new PlaceService(context);

		var result = await service.GetByIdAsync(place.Id);

		Assert.NotNull(result);
		Assert.Equal("Test Place", result.Name);
	}

	[Fact]
	public async Task GetFilteredAsync_ReturnsFilteredResults()
	{
		var context = GetInMemoryDbContext();
		context.Places.AddRange(
			new Catering { Name = "Cafe", Mark = 4.5, Type = 0 },
			new Sport { Name = "Gym", Mark = 3.0, Type = PlaceCategory.Sport }
		);
		await context.SaveChangesAsync();

		var service = new PlaceService(context);
		var result = await service.GetFilteredAsync(
			query: "ca",
			typeId: null,
			minMark: 4.0,
			minAge: null,
			categories: new List<PlaceCategory> { PlaceCategory.Catering },
			page: 1,
			pageSize: 10
		);

		Assert.Single(result.Items);
		Assert.Equal("Cafe", result.Items.First().Name);
	}

	[Fact]
	public async Task CreateAsync_AddsPlaceToDatabase()
	{
		var context = GetInMemoryDbContext();
		var service = new PlaceService(context);

		var newPlace = new Catering { Name = "New Place", Type = 0 };
		var created = await service.CreateAsync(newPlace);

		Assert.NotNull(created);
		Assert.Equal("New Place", created.Name);
		Assert.True(await context.Places.AnyAsync(p => p.Name == "New Place"));
	}

	[Fact]
	public async Task UpdateAsync_UpdatesExistingPlace()
	{
		var context = GetInMemoryDbContext();
		var place = new Catering { Name = "Old Name", Type = 0 };
		context.Places.Add(place);
		await context.SaveChangesAsync();

		var service = new PlaceService(context);
		place.Name = "Updated Name";
		var success = await service.UpdateAsync(place);

		Assert.True(success);
		var updated = await context.Places.FindAsync(place.Id);
		Assert.Equal("Updated Name", updated!.Name);
	}

	[Fact]
	public async Task DeleteAsync_RemovesPlaceFromDatabase()
	{
		var context = GetInMemoryDbContext();
		var place = new Catering { Name = "To Delete", Type = 0 };
		context.Places.Add(place);
		await context.SaveChangesAsync();

		var service = new PlaceService(context);
		var deleted = await service.DeleteAsync(place.Id);

		Assert.True(deleted);
		Assert.Null(await context.Places.FindAsync(place.Id));
	}
}