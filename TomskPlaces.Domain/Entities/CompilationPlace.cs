namespace TomskPlaces.Domain.Entities
{
	public class CompilationPlace
	{
		public Place Place { get; set; } = null!;
		public int PlaceId { get; set; }
		public Compilation Compilation { get; set; } = null!;
		public int CompilationId { get; set; }
		public int SequenceNumber { get; set; }
	}
}
