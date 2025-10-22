using Dealer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }	
		public DbSet<Category> Categories { get; set; }	
		public DbSet<User> Users { get; set; }	
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Configure relationships and constraints if needed
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.Cascade);
		}	
	}
}
