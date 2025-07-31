using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomskPlaces.Application.Common.Extensions;
using TomskPlaces.Application.Common.Models;
using TomskPlaces.Application.Interfaces.Services;
using TomskPlaces.Domain.Entities;
using TomskPlaces.Domain.Enums;
using TomskPlaces.Infrastructure.Persistence;

namespace TomskPlaces.Infrastructure.Services
{
	public class CateringService : ICateringService
	{
		private readonly ApplicationDbContext _context;

		public CateringService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Catering> CreateAsync(Catering catering)
		{
			_context.Caterings.Add(catering);
			await _context.SaveChangesAsync();
			return catering;
		}

		public async Task<bool> UpdateAsync(Catering catering)
		{
			var existing = await _context.Caterings
				.Include(c => c.Categories)
				.FirstOrDefaultAsync(c => c.Id == catering.Id);

			if (existing == null)
				return false;

			_context.Entry(existing).CurrentValues.SetValues(catering);

			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<PaginatedResult<Catering>> GetFilteredAsync(string? query,
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
			bool descending = false)
		{
			var placesQuery = _context.Caterings
				.Include(p => p.Images)
				.Include(p => p.Reviews)
				.AsQueryable();
			if (!string.IsNullOrWhiteSpace(query))
				placesQuery = placesQuery.Where(p => p.Name.ToLower().Contains(query.ToLower()));

			if (minMark != null)
				placesQuery = placesQuery.Where(p => p.Mark >= minMark);

			if (cuisine != null)
				placesQuery = placesQuery.Where(p => p.Cuisine == cuisine);
			
			if (cateringType != null)
				placesQuery = placesQuery.Where(p => p.CateringType == cateringType);
			
			if (hasTerrace != null)
				placesQuery = placesQuery.Where(p => p.HasTerrace == hasTerrace);
			
			if (hasVegan != null)
				placesQuery = placesQuery.Where(p => p.HasVegan == hasVegan);
			
			if (musicType != null)
				placesQuery = placesQuery.Where(p => p.Music == musicType);

			placesQuery = (sortBy?.ToLower()) switch
			{
				"name" => descending ? placesQuery.OrderByDescending(p => p.Name) : placesQuery.OrderBy(p => p.Name).ThenByDescending(p=>p.Mark),
				"mark" => descending ? placesQuery.OrderByDescending(p => p.Mark) : placesQuery.OrderBy(p => p.Mark).ThenBy(p => p.Name),
				_ => placesQuery.OrderByDescending(p => p.Mark).ThenBy(p => p.Name)
			};

			return await placesQuery.ToPaginatedResultAsync(page, pageSize);
		}
	}
}
