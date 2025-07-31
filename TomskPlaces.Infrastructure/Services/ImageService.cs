using Microsoft.AspNetCore.Http;
using TomskPlaces.Application.Interfaces.Services;
using TomskPlaces.Domain.Entities;
using TomskPlaces.Infrastructure.Persistence;

public class ImageService : IImageService
{
	private readonly ApplicationDbContext _context;

	public ImageService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<int> SaveImageInfoAsync(string fileName, string contentType, string url)
	{
		var image = new Image
		{
			FileName = fileName,
			ContentType = contentType,
			Url = url,
			UploadedAt = DateTime.UtcNow
		};

		_context.Images.Add(image);
		await _context.SaveChangesAsync();

		return image.Id;
	}

	public async Task<bool> DeleteImageInfoAsync(int id)
	{
		var image = await _context.Images.FindAsync(id);
		if (image == null) return false;

		_context.Images.Remove(image);
		await _context.SaveChangesAsync();

		return true;
	}
	public async Task<Image?> GetImageByIdAsync(int id)
	{
		return await _context.Images.FindAsync(id);
	}
}
