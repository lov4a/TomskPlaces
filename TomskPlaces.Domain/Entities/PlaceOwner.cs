namespace TomskPlaces.Domain.Entities
{
	public class PlaceOwner
	{
		public User User { get; set; } = null!;
		public Guid UserId { get; set; }
		public Place Place { get; set; } = null!;
		public int PlaceId { get; set; }
	}
}
