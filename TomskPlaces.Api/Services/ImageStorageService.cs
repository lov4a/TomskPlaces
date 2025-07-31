namespace TomskPlaces.Api.Services
{
	public class ImageStorageService
	{
		private readonly IWebHostEnvironment _env;

		public ImageStorageService(IWebHostEnvironment env)
		{
			_env = env;
		}

		public async Task<string> SaveFileAsync(IFormFile file)
		{
			var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
			Directory.CreateDirectory(uploadsDir);

			var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
			var filePath = Path.Combine(uploadsDir, uniqueFileName);

			using var stream = new FileStream(filePath, FileMode.Create);
			await file.CopyToAsync(stream);

			return $"/uploads/{uniqueFileName}";
		}

		public void DeleteFile(string relativePath)
		{
			var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/'));
			if (File.Exists(fullPath))
				File.Delete(fullPath);
		}
	}

}
