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
		public double mark { get; set; }
		public DateOnly Date {  get; set; } = DateOnly.FromDateTime(DateTime.Now);
		public TimeOnly Time {  get; set; } = TimeOnly.FromDateTime(DateTime.Now);
		public ICollection<Image> Images { get; set; } = new List<Image>();
	}
}
