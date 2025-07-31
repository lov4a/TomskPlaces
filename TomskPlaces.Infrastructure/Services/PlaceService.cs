using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomskPlaces.Domain.Entities;
using TomskPlaces.Infrastructure.Persistence;
using TomskPlaces.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using TomskPlaces.Application.Common.Models;
using TomskPlaces.Application.Common.Extensions;
using TomskPlaces.Domain.Enums;

namespace TomskPlaces.Infrastructure.Services
{
	public class PlaceService : IPlaceService
	{
		private readonly ApplicationDbContext _context;
		public PlaceService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Place?> GetByIdAsync(int id, PlaceCategory category)
		{
			return category switch
			{
				PlaceCategory.Catering => await _context.Caterings
					.Include(p => p.Images)
					.Include(p => p.Categories).ThenInclude(p=>p.Dishes)
					.Include(p => p.Reviews).ThenInclude(p => p.Images)
					.FirstOrDefaultAsync(p => p.Id == id),

				PlaceCategory.Sport => await _context.Sports
					.Include(p => p.Images)
					.Include(p => p.Reviews).ThenInclude(p => p.Images)
					.FirstOrDefaultAsync(p => p.Id == id),
				
				PlaceCategory.Entertainment => await _context.Entertainments
					.Include(p => p.Images)
					.Include(p => p.Reviews).ThenInclude(p => p.Images)
					.FirstOrDefaultAsync(p => p.Id == id),
				
				PlaceCategory.WalkPlace => await _context.WalkPlaces
					.Include(p => p.Images)
					.Include(p => p.Reviews).ThenInclude(p => p.Images)
					.FirstOrDefaultAsync(p => p.Id == id),

				PlaceCategory.Event => await _context.Events
					.Include(p => p.Images)
					.Include(p => p.Reviews).ThenInclude(p => p.Images)
					.FirstOrDefaultAsync(p => p.Id == id),
				
				_ => null
			};
		}
		public async Task<IEnumerable<Place>> GetAllAsync()
		{
			return await _context.Places
				.Include(i => i.Images)
				.Include(p => p.Reviews)
				.Include(p => p.Types)
				.ToListAsync();
		}
		public async Task<PaginatedResult<Place>> GetFilteredAsync(string? query, int? typeId, double? minMark,
			int? minAge, List<PlaceCategory>? categories, bool? isOpened, int page, int pageSize, string? sortBy = null,
			bool descending = false)
		{
			var placesQuery = _context.Places
				.Include(p => p.Images)
				.Include(p => p.Reviews)
				.Include(p => p.Schedules)
		.AsQueryable();
			if (!string.IsNullOrWhiteSpace(query))
				placesQuery = placesQuery.Where(p => p.Name.ToLower().Contains(query.ToLower()));

			if (categories != null && categories.Any())
				placesQuery = placesQuery.Where(p => categories.Contains(p.Type));

			if (minMark != null)
				placesQuery = placesQuery.Where(p => p.Mark >= minMark);

			if (minAge != null)
				placesQuery = placesQuery.Where(p => p.MinimumAge >= minAge);

			if (isOpened != null && isOpened == true)
			{
				var now = DateTime.UtcNow;
				var currentDay = now.DayOfWeek;
				var currentTime = now.TimeOfDay;

				placesQuery = placesQuery.Where(p =>
					p.Schedules != null && p.Schedules.Any(s =>
						s.DayOfWeek == currentDay &&
						!s.IsClosed &&
						s.OpenTime <= currentTime &&
						s.CloseTime > currentTime
					)
				);
			}

			placesQuery = (sortBy?.ToLower()) switch
			{
				"name" => descending ? placesQuery.OrderByDescending(p => p.Name) : placesQuery.OrderBy(p => p.Name).ThenByDescending(p=>p.Mark),
				"mark" => descending ? placesQuery.OrderByDescending(p => p.Mark) : placesQuery.OrderBy(p => p.Mark).ThenBy(p=>p.Name),
				_ => placesQuery.OrderByDescending(p => p.Mark).ThenBy(p => p.Name)
			};

			return await placesQuery.ToPaginatedResultAsync(page, pageSize);

		}

		public async Task<bool> DeleteAsync(int id)
		{
			var place = await _context.Places.FindAsync(id);
			if (place == null) return false;

			_context.Places.Remove(place);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
