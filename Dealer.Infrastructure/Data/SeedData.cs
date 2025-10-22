using Dealer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Infrastructure.Data;
public class SeedData
{
	public static async Task InitializeAsync(ApplicationDbContext context)
	{
		//Eğer veritabanı yoksa oluştur
		await context.Database.MigrateAsync();

		//eğer User tablosunda veri yoksa
		if (!context.Users.Any())
		{
			context.Users.AddRange(
				new User { UserName = "admin", PasswordHash = "12345", Email = "admin@dealer.com" },
				new User { UserName = "demo", PasswordHash = "demo123", Email = "demo@dealer.com" }
				);
		}

		// Category tablosu
		if (!context.Categories.Any())
		{
			context.Categories.AddRange(
				new Category { Name = "Electronics" },
				new Category { Name = "Books" },
				new Category { Name = "Clothing" }
			);
		}

		// Product tablosu
		if (!context.Products.Any())
		{
			context.Products.AddRange(
				new Product { Name = "Laptop", Price = 1500, CategoryId = 1 },
				new Product { Name = "T-Shirt", Price = 25, CategoryId = 3 },
				new Product { Name = "Novel", Price = 40, CategoryId = 2 }
			);
		}

		await context.SaveChangesAsync();
	}
}

