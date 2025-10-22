using Dealer.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Application.Interfaces
{
	public interface ICategoryService
	{
		Task<List<CategoryDto>> GetAllAsync();
		Task<CategoryDto?> GetByIdAsync(int id);
		Task<CategoryDto> CreateAsync(CategoryDto dto);
		Task UpdateAsync(int id, CategoryDto dto);
		Task DeleteAsync(int id);
	}
}
