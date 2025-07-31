using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomskPlaces.Application.Common.Models;
using TomskPlaces.Domain.Entities;

namespace TomskPlaces.Application.Interfaces.Services
{
	public interface IWalkPlaceService
	{
		Task<WalkPlace> CreateAsync(WalkPlace walkPlace);
		Task<bool> UpdateAsync(WalkPlace walkPlace);
		Task<PaginatedResult<WalkPlace>> GetFilteredAsync(
			string? query,
			int? typeId,
			double? minMark,
			int? duration,
			int page,
			int pageSize,
			string? sortBy = null,
			bool descending = false);
	}
}
