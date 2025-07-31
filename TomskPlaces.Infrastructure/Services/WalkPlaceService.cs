using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomskPlaces.Application.Common.Extensions;
using TomskPlaces.Application.Common.Models;
using TomskPlaces.Application.Interfaces.Services;
using TomskPlaces.Domain.Entities;
using TomskPlaces.Infrastructure.Persistence;

namespace TomskPlaces.Infrastructure.Services
{
	public class WalkPlaceService : IWalkPlaceService
	{
		private readonly ApplicationDbContext _context;

		public WalkPlaceService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<WalkPlace> CreateAsync(WalkPlace walkPlace)
		{
			_context.WalkPlaces.Add(walkPlace);
			await _context.SaveChangesAsync();
			return walkPlace;
		}
		public async Task<bool> UpdateAsync(WalkPlace walkPlace)
		{
			var existing = await _context.WalkPlaces
				.FirstOrDefaultAsync(c => c.Id == walkPlace.Id);

			if (existing == null)
				return false;

			_context.Entry(existing).CurrentValues.SetValues(walkPlace);

			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<PaginatedResult<WalkPlace>> GetFilteredAsync(
			string? query,
			int? typeId,
			double? minMark,
			int? duration,
			int page,
			int pageSize,
			string? sortBy = null,
			bool descending = false)
		{
			var placesQuery = _context.WalkPlaces
				.Include(p => p.Images)
				.Include(p => p.Reviews)
				.AsQueryable();
			if (!string.IsNullOrWhiteSpace(query))
				placesQuery = placesQuery.Where(p => p.Name.ToLower().Contains(query.ToLower()));

			if (minMark != null)
				placesQuery = placesQuery.Where(p => p.Mark >= minMark);

			if (duration != null)
				placesQuery = placesQuery.Where(p => p.Duration <= duration);


			placesQuery = (sortBy?.ToLower()) switch
			{
				"name" => descending ? placesQuery.OrderByDescending(p => p.Name) : placesQuery.OrderBy(p => p.Name).ThenByDescending(p => p.Mark),
				"mark" => descending ? placesQuery.OrderByDescending(p => p.Mark) : placesQuery.OrderBy(p => p.Mark).ThenBy(p => p.Name),
				"duration" => descending ? placesQuery.OrderByDescending(p => p.Duration) : placesQuery.OrderBy(p => p.Duration).ThenBy(p => p.Mark),
				_ => placesQuery.OrderByDescending(p => p.Mark).ThenBy(p => p.Name)
			};

			return await placesQuery.ToPaginatedResultAsync(page, pageSize);
		}
	}
}
