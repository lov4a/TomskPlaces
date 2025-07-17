namespace TomskPlaces.Domain.Entities
{
	public class Review
	{
		public int Id { get; set; }
		public Place Place { get; set; } = null!;
		public int PlaceId { get; set; }
		public User User { get; set; } = null!;
		public Guid UserId { get; set; }
		public string? Text { get; set; }
		public double Mark { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public ICollection<Image> Images { get; set; } = new List<Image>();
	}
}
