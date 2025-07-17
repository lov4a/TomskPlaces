namespace TomskPlaces.Domain.Entities
{
	public abstract class Place
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = "Описание";
		public string Adress { get; set; } = "г.Томск";
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string Site { get; set; } = "https://";
		public double? Mark { get; set; }
		public double MinimumAge { get; set; } = 0;
		public ICollection<PlaceOwner>? PlaceOwners { get; set; } = new List<PlaceOwner>();
		public ICollection<PlacePhoneNumber>? PhoneNumbers { get; set; } = new List<PlacePhoneNumber>();
		public ICollection<PlaceNetwork>? Networks { get; set; } = new List<PlaceNetwork>();
		public ICollection<WorkSchedule>? Schedules { get; set; } = new List<WorkSchedule>();
		public ICollection<Review>? Reviews { get; set; } = new List<Review>();
		public ICollection<PlaceImage>? Images { get; set; } = new List<PlaceImage>();
		public ICollection<PlaceType>? Types { get; set; }
		public ICollection<RoutePlace>? Routes { get; set; }

	}
}
