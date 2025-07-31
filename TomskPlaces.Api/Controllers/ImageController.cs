using Microsoft.AspNetCore.Mvc;
using TomskPlaces.Api.Services;
using TomskPlaces.Application.Interfaces.Services;

namespace TomskPlaces.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
	private readonly ImageStorageService _storageService;
	private readonly IImageService _imageService;

	public ImageController(ImageStorageService storageService, IImageService imageService)
	{
		_storageService = storageService;
		_imageService = imageService;
	}

	[HttpPost("upload")]
	public async Task<IActionResult> Upload(IFormFile file)
	{
		if (file == null || file.Length == 0)
			return BadRequest("Файл не выбран.");

		var relativePath = await _storageService.SaveFileAsync(file);

		var id = await _imageService.SaveImageInfoAsync(
			Path.GetFileName(file.FileName),
			file.ContentType,
			relativePath
		);

		return Ok(new { id, url = relativePath });
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var image = await _imageService.GetImageByIdAsync(id);
		if (image == null) return NotFound();

		_storageService.DeleteFile(image.Url);
		var result = await _imageService.DeleteImageInfoAsync(id);

		return result ? Ok() : StatusCode(500);
	}
}
