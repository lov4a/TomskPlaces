namespace TomskPlaces.Domain.Entities
{
	public class Compilation
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public Image Image { get; set; } = null!;
		public int ImageId { get; set; }
		public ICollection<CompilationPlace>? Places { get; set; } = new List<CompilationPlace>();
	}
}
