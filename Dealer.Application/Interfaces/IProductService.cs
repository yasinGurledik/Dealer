using Dealer.Application.DTOs;
using Dealer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Application.Interfaces
{
	public interface IProductService
	{
		Task<List<ProductDto>> GetAllAsync();
		Task<List<ProductDto>> GetFilterAsync(Expression<Func<Product, bool>>? filter = null);
		Task<List<ProductDto>> GetFilterAndIncludeAsync(Expression<Func<Product, bool>>? filter = null, params Expression<Func<Product, object>>[] includeProperties);
		Task<ProductDto?> GetByIdAsync(int id);
		Task<ProductDto> CreateAsync(ProductDto dto);
		Task UpdateAsync(int id, ProductDto dto);
		Task DeleteAsync(int id);
	}
}
