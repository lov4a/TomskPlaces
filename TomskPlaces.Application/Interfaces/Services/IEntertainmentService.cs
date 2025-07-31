using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomskPlaces.Application.Common.Models;
using TomskPlaces.Domain.Entities;
using TomskPlaces.Domain.Enums;

namespace TomskPlaces.Application.Interfaces.Services
{
	public interface IEntertainmentService
	{
		Task<Entertainments> CreateAsync(Entertainments entertainments);
		Task<bool> UpdateAsync(Entertainments entertainments);
		Task<PaginatedResult<Entertainments>> GetFilteredAsync(
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
