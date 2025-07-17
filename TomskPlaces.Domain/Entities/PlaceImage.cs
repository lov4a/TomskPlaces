namespace TomskPlaces.Domain.Entities
{
	public class PlaceImage
	{
		public Place Place { get; set; } = null!;
		public int PlaceId { get; set; }
		public Image Image { get; set; } = null!;
		public int ImageId { get; set; }
		public int SequenceNumber { get; set; }
	}
}
