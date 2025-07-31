using Microsoft.AspNetCore.Http;
using TomskPlaces.Domain.Entities;

namespace TomskPlaces.Application.Interfaces.Services
{
	public interface IImageService
	{
		Task<int> SaveImageInfoAsync(string fileName, string contentType, string url);
		Task<bool> DeleteImageInfoAsync(int imageId);
		Task<Image?> GetImageByIdAsync(int id);
	}
}
