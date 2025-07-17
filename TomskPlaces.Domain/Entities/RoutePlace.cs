namespace TomskPlaces.Domain.Entities
{
	public class RoutePlace
	{
		public Place Place { get; set; } = null!;
		public int PlaceId { get; set; }
		public Route Route { get; set; } = null!;
		public int RouteId { get; set; }
	}
}
