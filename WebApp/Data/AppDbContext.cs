using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
	public class AppDbContext:DbContext
	{
		public DbSet <Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ApplicationType> ApplicationTypes { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			//Database.EnsureDeleted();
		}
	}
}
