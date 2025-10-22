using AutoMapper;
using Dealer.Application.DTOs;
using Dealer.Application.Interfaces;
using Dealer.Domain.Entities;
using Dealer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IRepository<Category> _repository;
		private readonly IMapper _mapper;

		public CategoryService(IRepository<Category> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<CategoryDto> CreateAsync(CategoryDto dto)
		{
			var entity = _mapper.Map<Category>(dto);
			await _repository.AddAsync(entity);
			return _mapper.Map<CategoryDto>(entity);
		}

		public async Task DeleteAsync(int id)
		{
			var existing = await _repository.GetByIdAsync(id);
			if (existing == null) throw new Exception("Category not found");
			await _repository.DeleteAsync(existing);
		}

		public async Task<List<CategoryDto>> GetAllAsync()
		{
			var products = await _repository.GetAllAsync();
			return _mapper.Map<List<CategoryDto>>(products);
		}

		public async Task<CategoryDto?> GetByIdAsync(int id)
		{
			var p = await _repository.GetByIdAsync(id);
			return p is null ? null : _mapper.Map<CategoryDto>(p); 
		}

		public async Task UpdateAsync(int id, CategoryDto dto)
		{
			var existing = await _repository.GetByIdAsync(id);
			if (existing == null) throw new Exception("Category not found");
			_mapper.Map(dto, existing);
			await _repository.UpdateAsync(existing);
		}
	}
}
