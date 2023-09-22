using System;
using Microsoft.EntityFrameworkCore;

namespace ReviewsAzureFunctionsApi
{
	public class ReviewsDbContext : DbContext
	{
		public ReviewsDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<Review> Reviews { get; set; }
	}
}

