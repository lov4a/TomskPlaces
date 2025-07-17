namespace TomskPlaces.Domain.Entities
{
	public class ReviewImage
	{
		public Review Review { get; set; } = null!;
		public int ReviewId { get; set; }
		public Image Image { get; set; } = null!;
		public int ImageId { get; set; }
		public int SequenceNumber { get; set; }
	}
}
