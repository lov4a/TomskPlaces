using Microsoft.EntityFrameworkCore;
using TomskPlaces.Application.Common.Models;

namespace TomskPlaces.Application.Common.Extensions
{
	public static class PaginatedResultExtensions
	{
		public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>( this IQueryable<T> query, int page, int pageSize, CancellationToken cancellationToken = default)
		{
			if (page <= 0) page = 1;
			if (pageSize <= 0) pageSize = 10;

			var totalCount = await query.CountAsync(cancellationToken);

			var items = await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync(cancellationToken);

			return new PaginatedResult<T>(items, totalCount, page, pageSize);
		}
	}
}
