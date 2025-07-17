using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomskPlaces.Domain.Entities
{
	public class Network
	{
		public int Id { get; set; }
		public string Name { get; set; } = "СоцСеть";
		public Image Image { get; set; } = null!;
		public int ImageId { get; set; }
		public ICollection<PlaceNetwork> Places { get; set; } = new List<PlaceNetwork>();
	}
}
