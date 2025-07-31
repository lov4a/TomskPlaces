using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomskPlaces.Application.Common.Models;
using TomskPlaces.Domain.Entities;

namespace TomskPlaces.Application.Interfaces.Services
{
	public interface IEventService
	{
		Task<Event> CreateAsync(Event _event);
		Task<bool> UpdateAsync(Event _event);
		Task<PaginatedResult<Event>> GetFilteredAsync(
			string? query,
			int? typeId,
			double? minMark,
			int? duration,
			DateTime? startDate,
			DateTime? endDate,
			int page,
			int pageSize,
			string? sortBy = null,
			bool descending = false);
	}
}
