namespace TomskPlaces.Domain.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public string UserName { get; set; } = null!;
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public Image? Image { get; set; }
		public int? ImageId { get; set; }
		public ICollection<PlaceOwner>? PlaceOwned { get; set; } = new List<PlaceOwner>();
	}
}
