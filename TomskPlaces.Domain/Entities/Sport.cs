namespace TomskPlaces.Domain.Entities
{
	public class Sport : Place
	{
		public int Duration { get; set; } // в минутах
		public int MinNumberOfpeople { get; set; }
		public int MaxNumberOfpeople { get; set; }
	}
}
