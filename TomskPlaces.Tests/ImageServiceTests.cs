using Microsoft.EntityFrameworkCore;
using TomskPlaces.Infrastructure.Persistence;
using TomskPlaces.Infrastructure.Services;
using Xunit;

namespace TomskPlaces.Tests;

public class ImageServiceTests
{
	private ApplicationDbContext GetInMemoryDbContext()
	{
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

		return new ApplicationDbContext(options);
	}

	[Fact]
	public async Task SaveImageInfoAsync_SavesMetadataCorrectly()
	{
		// Arrange
		var context = GetInMemoryDbContext();
		var service = new ImageService(context);

		var fileName = "example.jpg";
		var contentType = "image/jpeg";
		var url = "/uploads/example.jpg";

		// Act
		var imageId = await service.SaveImageInfoAsync(fileName, contentType, url);

		// Assert
		var savedImage = await context.Images.FindAsync(imageId);
		Assert.NotNull(savedImage);
		Assert.Equal(fileName, savedImage.FileName);
		Assert.Equal(contentType, savedImage.ContentType);
		Assert.Equal(url, savedImage.Url);
	}
}
