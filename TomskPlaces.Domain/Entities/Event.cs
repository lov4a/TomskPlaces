namespace TomskPlaces.Domain.Entities
{
	public class Event: Place
	{
		public int Duration { get; set; }
		public int NumberOfPeop { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}
