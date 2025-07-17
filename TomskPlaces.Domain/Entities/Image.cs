using System.ComponentModel.DataAnnotations;

namespace TomskPlaces.Domain.Entities
{
	public class Image
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string FileName { get; set; } = null!;

		[Required]
		public string ContentType { get; set; } = "image/jpeg";

		[Required]
		public string Url { get; set; } = null!;

		public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
	}
}
