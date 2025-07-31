using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomskPlaces.Application.Common.Models;
using TomskPlaces.Domain.Entities;

namespace TomskPlaces.Application.Interfaces.Services
{
	public interface ISportService
	{
		Task<Sport> CreateAsync(Sport sport);
		Task<bool> UpdateAsync(Sport sport);
		Task<PaginatedResult<Sport>> GetFilteredAsync(
			string? query,
			int? typeId,
			double? minMark,
			int? duration,
			int? minNumberOfpeople,
			int? maxNumberOfpeople,
			int page,
			int pageSize,
			string? sortBy = null,
			bool descending = false);
	}
}
