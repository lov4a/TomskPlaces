using TomskPlaces.Application.Common.Models;
using TomskPlaces.Domain.Entities;
using TomskPlaces.Domain.Enums;

namespace TomskPlaces.Application.Interfaces.Services
{
	public interface IPlaceService
	{
		// READ
		Task<Place?> GetByIdAsync(int id, PlaceCategory category);
		Task<PaginatedResult<Place>> GetFilteredAsync(
			string? query,
			int? typeId,
			double? minMark,
			int? minAge,
			List<PlaceCategory>? categories, // Множественный фильтр по типам
			bool? isOpened,
			int page,
			int pageSize,
			string? sortBy = null,
			bool descending = false);
		Task<IEnumerable<Place>> GetAllAsync();

		// DELETE
		Task<bool> DeleteAsync(int id);
	}
}
