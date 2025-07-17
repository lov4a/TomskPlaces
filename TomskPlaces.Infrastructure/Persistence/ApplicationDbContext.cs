using Microsoft.EntityFrameworkCore;
using TomskPlaces.Domain.Entities;

namespace TomskPlaces.Infrastructure.Persistence
{
	public class ApplicationDbContext :DbContext 
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Place> Places { get; set; } = null!;
		public DbSet<PlacePhoneNumber> PhoneNumbers { get; set; } = null!;
		public DbSet<Network> Networks { get; set; } = null!;
		public DbSet<PlaceNetwork> PlaceNetworks { get; set; } = null!;
		public DbSet<PlaceOwner> PlaceOwners { get; set; } = null!;
		public DbSet<WorkSchedule> WorkSchedules { get; set; } = null!;
		public DbSet<Review> Reviews { get; set; } = null!;
		public DbSet<Image> Images { get; set; } = null!;
		public DbSet<ReviewImage> ReviewsImages { get; set; } = null!;
		public DbSet<PlaceImage> PlaceImages { get; set; } = null!;
		public DbSet<Catering> Caterings { get; set; } = null!;
		public DbSet<Entertainments> Entertainments { get; set;} = null!;
		public DbSet<Sport> Sports { get; set; } = null!;
		public DbSet<WalkPlace> WalkPlaces { get; set; } = null!;
		public DbSet<TypeOfPlace> Types { get; set; } = null!;
		public DbSet<PlaceType> PlaceTypes { get; set; } = null!;
		public DbSet<MenuCategory> MenuCategories { get; set; } = null!;
		public DbSet<CategoryDish> CategoryDish { get; set; } = null!;
		public DbSet<Route> Route { get; set; } = null!;
		public DbSet<RoutePlace> RoutePlace { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Place>().ToTable("Places");
			modelBuilder.Entity<Catering>().ToTable("Caterings");
			modelBuilder.Entity<Entertainments>().ToTable("Entertainments");
			modelBuilder.Entity<Sport>().ToTable("Sports");
			modelBuilder.Entity<WalkPlace>().ToTable("WalkPlaces");

			modelBuilder.Entity<PlaceOwner>()
				.HasKey(po => new { po.PlaceId, po.UserId });
			modelBuilder.Entity<PlaceOwner>()
				.HasOne(o => o.Place)
				.WithMany(o => o.PlaceOwners)
				.HasForeignKey(o => o.PlaceId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<PlaceOwner>()
				.HasOne(po => po.User)
				.WithMany(po => po.PlaceOwned)
				.HasForeignKey(po => po.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PlacePhoneNumber>()
				.HasOne(ppn => ppn.Place)
				.WithMany(ppn => ppn.PhoneNumbers)
				.HasForeignKey(ppn => ppn.PlaceId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PlaceNetwork>()
				.HasKey(pn => new { pn.PlaceId, pn.NetworkId });
			modelBuilder.Entity<PlaceNetwork>()
				.HasOne(pn => pn.Place)
				.WithMany(pn => pn.Networks)
				.HasForeignKey(pn => pn.PlaceId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<PlaceNetwork>()
				.HasOne(pn => pn.Network)
				.WithMany(pn => pn.Places)
				.HasForeignKey(pn => pn.NetworkId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<WorkSchedule>()
				.HasOne(ws => ws.Place)
				.WithMany(ws => ws.Schedules)
				.HasForeignKey(ws => ws.PlaceId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Review>()
				.HasOne(r => r.User)
				.WithMany(r => r.Reviews)
				.HasForeignKey(r => r.UserId)
				.OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<Review>()
				.HasOne(r => r.Place)
				.WithMany(r => r.Reviews)
				.HasForeignKey(r => r.PlaceId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ReviewImage>()
				.HasKey(ri => new { ri.ReviewId, ri.ImageId });
			modelBuilder.Entity<ReviewImage>()
				.HasOne(ri => ri.Review)
				.WithMany(ri => ri.Images)
				.HasForeignKey(ri => ri.ReviewId)
				.OnDelete(DeleteBehavior.Cascade);


			modelBuilder.Entity<PlaceImage>()
				.HasKey(pi => new { pi.PlaceId, pi.ImageId });
			modelBuilder.Entity<PlaceImage>()
				.HasOne(pi => pi.Place)
				.WithMany(pi => pi.Images)
				.HasForeignKey(pi => pi.PlaceId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PlaceType>()
				.HasKey(pt => new { pt.PlaceId, pt.TypeOfPlaceId });
			modelBuilder.Entity<PlaceType>()
				.HasOne(pt => pt.Place)
				.WithMany(pt => pt.Types)
				.HasForeignKey(pt => pt.PlaceId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<PlaceType>()
				.HasOne(pt => pt.TypeOfPlace)
				.WithMany(pt => pt.Places)
				.HasForeignKey(pt => pt.TypeOfPlaceId)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<RoutePlace>()
				.HasKey(rp => new { rp.PlaceId, rp.RouteId });
			modelBuilder.Entity<RoutePlace>()
				.HasOne(rp => rp.Place)
				.WithMany(rp => rp.Routes)
				.HasForeignKey(rp => rp.PlaceId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<RoutePlace>()
				.HasOne(rp => rp.Route)
				.WithMany(rp => rp.Places)
				.HasForeignKey(rp => rp.RouteId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<MenuCategory>()
				.HasOne(mc => mc.Catering)
				.WithMany(mc => mc.Categories)
				.HasForeignKey(mc => mc.CateringId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<CategoryDish>()
				.HasOne(cd => cd.MenuCategory)
				.WithMany(cd => cd.Dishes)
				.HasForeignKey(cd => cd.CategoryId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
