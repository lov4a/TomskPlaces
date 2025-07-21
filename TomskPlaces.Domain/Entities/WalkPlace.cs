using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomskPlaces.Domain.Entities
{
	public class WalkPlace : Place
	{
		public int Duration { get; set; } // в минутах
	}
}
