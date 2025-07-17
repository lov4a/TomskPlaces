namespace TomskPlaces.Domain.Entities
{
	public class Route
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public int Duration {  get; set; }
		public double Distance { get; set; }
		public char Transport {  get; set; }
		public Image Image { get; set; } = null!;
		public int ImageId { get; set; }
		public ICollection<RoutePlace>? Places { get; set; }

	}
}
