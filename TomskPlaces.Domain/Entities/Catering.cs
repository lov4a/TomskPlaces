using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomskPlaces.Domain.Enums;

namespace TomskPlaces.Domain.Entities
{
	public class Catering : Place
	{
		public string? Cuisine { get; set; } // кухня: итальянская, азиатская...
		public CateringType Type { get; set; } // тип: кафе, ресторан...
		public bool HasTerrace { get; set; }
		public MusicType? Music { get; set; }  //музыка: живая, неживая, нет.
		public bool HasVegan { get; set; }
		public ICollection<MenuCategory> Categories { get; set; } = new List<MenuCategory>();

	}
}
