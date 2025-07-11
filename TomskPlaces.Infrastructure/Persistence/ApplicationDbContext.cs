using Microsoft.EntityFrameworkCore;

namespace TomskPlaces.Infrastructure.Persistence
{
	public class ApplicationDbContext :DbContext 
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

	}
}
