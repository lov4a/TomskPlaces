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
	public class EventService : IEventService
	{
		private readonly ApplicationDbContext _context;

		public EventService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Event> CreateAsync(Event _event)
		{
			_context.Events.Add(_event);
			await _context.SaveChangesAsync();
			return _event;
		}
		public async Task<bool> UpdateAsync(Event _event)
		{
			var existing = await _context.Events
				.FirstOrDefaultAsync(c => c.Id == _event.Id);

			if (existing == null)
				return false;

			_context.Entry(existing).CurrentValues.SetValues(_event);

			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<PaginatedResult<Event>> GetFilteredAsync(
			string? query,
			int? typeId,
			double? minMark,
			int? duration,
			DateTime? startDate,
			DateTime? endDate,
			int page,
			int pageSize,
			string? sortBy = null,
			bool descending = false)
		{
			var placesQuery = _context.Events
				.Include(p => p.Images)
				.Include(p => p.Reviews)
				.AsQueryable();
			if (!string.IsNullOrWhiteSpace(query))
				placesQuery = placesQuery.Where(p => p.Name.ToLower().Contains(query.ToLower()));

			if (minMark != null)
				placesQuery = placesQuery.Where(p => p.Mark >= minMark);

			if (duration != null)
				placesQuery = placesQuery.Where(p => p.Duration <= duration);
			if (startDate != null)
				placesQuery = placesQuery.Where(p => p.StartDate >= startDate);
			if (endDate != null)
				placesQuery = placesQuery.Where(p => p.EndDate <= endDate);


			placesQuery = (sortBy?.ToLower()) switch
			{
				"name" => descending ? placesQuery.OrderByDescending(p => p.Name) : placesQuery.OrderBy(p => p.Name).ThenByDescending(p => p.Mark),
				"mark" => descending ? placesQuery.OrderByDescending(p => p.Mark) : placesQuery.OrderBy(p => p.Mark).ThenBy(p => p.Name),
				"duration" => descending ? placesQuery.OrderByDescending(p => p.Duration) : placesQuery.OrderBy(p => p.Duration).ThenBy(p => p.Mark),
				"date" => descending ? placesQuery.OrderByDescending(p => p.StartDate) : placesQuery.OrderBy(p => p.StartDate).ThenBy(p => p.Mark),
				_ => placesQuery.OrderByDescending(p => p.Mark).ThenBy(p => p.Name)
			};

			return await placesQuery.ToPaginatedResultAsync(page, pageSize);
		}
	}
}
