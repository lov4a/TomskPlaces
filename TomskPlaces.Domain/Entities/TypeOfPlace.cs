namespace TomskPlaces.Domain.Entities
{
	public class TypeOfPlace
	{
		public int Id { get; set; }
		public char Type { get; set; }// c - споррт, e - развлечения, w - прогулки.
		public string Name { get; set; } = null!;
		public Image Image { get; set; } = null!;
		public int ImageId { get; set; }
		public string? Discription { get; set; }
		public ICollection<PlaceType>? Places { get; set; }
	}
}
