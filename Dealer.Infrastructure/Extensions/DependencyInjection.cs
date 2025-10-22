using Dealer.Domain.Interfaces;
using Dealer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Infrastructure.Extensions
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
				b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			return services;
		}
	}
}
