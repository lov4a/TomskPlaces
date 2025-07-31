using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomskPlaces.Application.Common.Extensions;
using TomskPlaces.Application.Common.Models;
using TomskPlaces.Domain.Entities;
using TomskPlaces.Infrastructure.Persistence;

namespace TomskPlaces.Infrastructure.Services
{
	public class SportService
	{
		private readonly ApplicationDbContext _context;

		public SportService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Sport> CreateAsync(Sport sport)
		{
			_context.Sports.Add(sport);
			await _context.SaveChangesAsync();
			return sport;
		}
		public async Task<bool> UpdateAsync(Sport sport)
		{
			var existing = await _context.Sports
				.FirstOrDefaultAsync(c => c.Id == sport.Id);

			if (existing == null)
				return false;

			_context.Entry(existing).CurrentValues.SetValues(sport);

			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<PaginatedResult<Sport>> GetFilteredAsync(
			string? query,
			int? typeId,
			double? minMark,
			int? duration,
			int? minNumberOfpeople,
			int? maxNumberOfpeople,
			int page,
			int pageSize,
			string? sortBy = null,
			bool descending = false)
		{
			var placesQuery = _context.Sports
				.Include(p => p.Images)
				.Include(p => p.Reviews)
				.AsQueryable();
			if (!string.IsNullOrWhiteSpace(query))
				placesQuery = placesQuery.Where(p => p.Name.ToLower().Contains(query.ToLower()));

			if (minMark != null)
				placesQuery = placesQuery.Where(p => p.Mark >= minMark);

			if (duration != null)
				placesQuery = placesQuery.Where(p => p.Duration <= duration);

			if (minNumberOfpeople != null)
				placesQuery = placesQuery.Where(p => p.MinNumberOfpeople >= minNumberOfpeople);

			if (maxNumberOfpeople != null)
				placesQuery = placesQuery.Where(p => p.MinNumberOfpeople <= maxNumberOfpeople);

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
