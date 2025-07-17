namespace TomskPlaces.Domain.Entities
{
	public class MenuCategory
	{
		public int Id { get; set; }
		public Catering Catering { get; set; } = null!;
		public int CateringId { get; set; }
		public string CategoryName { get; set; } = "Категория меню";
		public int SequenceNumber { get; set; }
	}
}
