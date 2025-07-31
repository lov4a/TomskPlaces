using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomskPlaces.Application.Common.Models
{
	public class PaginatedResult<T>
	{
		public IEnumerable<T> Items { get; }
		public int TotalCount { get; }      
		public int Page { get; }            
		public int PageSize { get; }        

		public PaginatedResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
		{
			Items = items;
			TotalCount = totalCount;
			Page = page;
			PageSize = pageSize;
		}
	}
}
