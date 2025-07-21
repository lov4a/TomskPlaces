namespace TomskPlaces.Domain.Entities
{
	public class News
	{
		public int Id { get; set; }
		public string? Title { get; set; } = null!;
		public string? Preview { get; set; }
		public string? Text { get; set; } = null!;
		public Image MainImage { get; set; } = null!;
		public Image? MobileImage { get; set; } = null!;
		public int MainImageId { get; set; }
		public int? MobileImageId { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public int? Number { get; set; }
		public bool IsBanner { get; set; }
		public string? Link { get; set; }
	}
}
