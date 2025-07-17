namespace TomskPlaces.Domain.Entities
{
	public class PlacePhoneNumber
	{
		public int Id { get; set; }
		public Place Place { get; set; } = null!;
		public int PlaceId { get; set; }
		public string Phone { get; set; } = "+7";
		public string Discription { get; set; } = "Запись";
		public int SequenceNumber { get; set; }
	}
}
