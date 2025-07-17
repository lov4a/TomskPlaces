using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomskPlaces.Domain.Entities
{
	public class CategoryDish
	{
		public int Id { get; set; }
		public MenuCategory? MenuCategory { get; set; }
		public int? CategoryId { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public double Weight { get; set; }
		public double Price { get; set; }
		public string? Remark { get; set; }
		public Image? Image { get; set; } = null!;
		public int? ImageId { get; set; }
	}
}
