namespace TomskPlaces.Domain.Entities
{
	public class PlaceType
	{
		public Place Place { get; set; } = null!;
		public int PlaceId { get; set; }
		public TypeOfPlace TypeOfPlace { get; set; } = null!;
		public int TypeOfPlaceId { get; set; }
	}
}
