using AutoMapper;
using Dealer.Application.DTOs;
using Dealer.Application.Interfaces;
using Dealer.Domain.Entities;
using Dealer.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace Dealer.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IRepository<User> _repository;
		private readonly IMapper _mapper;
		private readonly IJwtService _jwtService;

		public UserService(IRepository<User> repository, IMapper mapper, IJwtService jwtService)
		{
			_repository = repository;
			_mapper = mapper;
			_jwtService = jwtService;
		}
		public async Task<UserDto> CreateAsync(UserDto dto)
		{
			var entity = _mapper.Map<User>(dto);
			await _repository.AddAsync(entity);
			return _mapper.Map<UserDto>(entity);
		}

		public async Task DeleteAsync(int id)
		{
			var existing = await _repository.GetByIdAsync(id);
			if (existing == null) throw new Exception("User not found");
			await _repository.DeleteAsync(existing);
		}

		public async Task<List<UserDto>> GetAllAsync()
		{
			var products = await _repository.GetAllAsync();
			return _mapper.Map<List<UserDto>>(products);
		}

		public async Task<UserDto?> GetByIdAsync(int id)
		{
			var p = await _repository.GetByIdAsync(id);
			return p is null ? null : _mapper.Map<UserDto>(p); 
		}

		public async Task UpdateAsync(int id, UserDto dto)
		{
			var existing = await _repository.GetByIdAsync(id);
			if (existing == null) throw new Exception("User not found");
			_mapper.Map(dto, existing);
			await _repository.UpdateAsync(existing);
		}
		public async Task<List<UserDto>> GetFilterAsync(Expression<Func<User, bool>>? filter = null)
		{
			var products = await _repository.GetFilterAsync(filter);
			return _mapper.Map<List<UserDto>>(products);
		}

		public async Task<UserDto> RegisterAsync(UserRegisterDto dto)
		{
			var user = _mapper.Map<User>(dto);
			// Şifreyi hashleyebilirsin
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password /*, workFactor: 12 */);

			await _repository.AddAsync(user);
			return _mapper.Map<UserDto>(user);
		}

		public async Task<string?> LoginAsync(UserLoginDto dto)
		{
			var user = (await _repository.GetFilterAsync(u => u.UserName == dto.UserName)).FirstOrDefault();
			if (user == null) return null;

			bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
			if (!isPasswordValid) return null;

			return _jwtService.GenerateToken(user.Id, user.UserName);
		}
	
	}
}
