using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomskPlaces.Domain.Entities
{
	public class WorkSchedule
	{
		public int Id { get; set; }

		public int PlaceId { get; set; }
		public Place Place { get; set; } = null!;

		public DayOfWeek DayOfWeek { get; set; }

		public TimeOnly OpenTime { get; set; }
		public TimeOnly CloseTime { get; set; }

		public bool IsClosed { get; set; } = false;
	}
}
