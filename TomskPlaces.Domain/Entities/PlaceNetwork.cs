using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomskPlaces.Domain.Entities
{
	public class PlaceNetwork
	{
		public Place Place { get; set; } = null!;
		public int PlaceId { get; set; }
		public Network Network { get; set; } = null!;
		public int NetworkId { get; set; }
	}
}
