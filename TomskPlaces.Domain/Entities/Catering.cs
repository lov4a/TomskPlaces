using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomskPlaces.Domain.Entities
{
	public class Catering : Place
	{
		public string? Cuisine { get; set; } // кухня: итальянская, азиатская...
		public char Type { get; set; } // тип: кафе, ресторан...
		public bool HasTerrace { get; set; } = false;
		public char? music { get; set; }  //музыка: живая, неживая, нет.
		public bool HasVegan { get; set; } = false;
		public ICollection<MenuCategory> Categories { get; set; } = new List<MenuCategory>();

	}
}
