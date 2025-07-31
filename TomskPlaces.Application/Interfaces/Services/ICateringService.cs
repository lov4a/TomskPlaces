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
	public interface ICateringService
	{
		Task<Catering> CreateAsync(Catering catering);
		Task<bool> UpdateAsync(Catering catering);
		Task<PaginatedResult<Catering>> GetFilteredAsync(
			string? query,
			int? typeId,
			double? minMark,
			string? cuisine,
			CateringType? cateringType,
			bool? hasTerrace,
			MusicType? musicType,
			bool? hasVegan,
			int page,
			int pageSize,
			string? sortBy = null,
			bool descending = false);
	}
}
