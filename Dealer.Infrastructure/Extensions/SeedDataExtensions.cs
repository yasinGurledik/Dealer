using Dealer.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Infrastructure.Extensions
{
	public static class SeedDataExtensions
	{
		public static async Task SeedDataAsync(this IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<Data.ApplicationDbContext>();
			await SeedData.InitializeAsync(context);
		}	
	}
}
