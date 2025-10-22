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
	public interface IUserService
	{
		Task<List<UserDto>> GetAllAsync();
		Task<List<UserDto>> GetFilterAsync(Expression<Func<User, bool>>? filter = null);
		Task<UserDto?> GetByIdAsync(int id);
		Task<UserDto> CreateAsync(UserDto dto);
		Task UpdateAsync(int id, UserDto dto);
		Task DeleteAsync(int id);
		Task<UserDto> RegisterAsync(UserRegisterDto dto);
		Task<string?> LoginAsync(UserLoginDto dto); // JWT token dönecek
	}
}
