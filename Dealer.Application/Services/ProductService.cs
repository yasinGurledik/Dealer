using AutoMapper;
using Dealer.Application.DTOs;
using Dealer.Application.Interfaces;
using Dealer.Domain.Entities;
using Dealer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IRepository<Product> _repository;
		private readonly IMapper _mapper;

		public ProductService(IRepository<Product> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<ProductDto> CreateAsync(ProductDto dto)
		{
			var entity = _mapper.Map<Product>(dto);
			await _repository.AddAsync(entity);
			return _mapper.Map<ProductDto>(entity);
		}

		public async Task DeleteAsync(int id)
		{
			var existing = await _repository.GetByIdAsync(id);
			if (existing == null) throw new Exception("Product not found");
			await _repository.DeleteAsync(existing);
		}

		public async Task<List<ProductDto>> GetAllAsync()
		{
			var products = await _repository.GetAllAsync();
			return _mapper.Map<List<ProductDto>>(products);
		}

		public async Task<ProductDto?> GetByIdAsync(int id)
		{
			var p = await _repository.GetByIdAsync(id);
			return p is null ? null : _mapper.Map<ProductDto>(p); 
		}

		public async Task UpdateAsync(int id, ProductDto dto)
		{
			var existing = await _repository.GetByIdAsync(id);
			if (existing == null) throw new Exception("Product not found");
			_mapper.Map(dto, existing);
			await _repository.UpdateAsync(existing);
		}

		public async Task<List<ProductDto>> GetFilterAsync(Expression<Func<Product, bool>>? filter = null)
		{
			var products = await _repository.GetFilterAsync(filter);
			return _mapper.Map<List<ProductDto>>(products);
		}

		public async Task<List<ProductDto>> GetFilterAndIncludeAsync(
			Expression<Func<Product, bool>>? filter = null,
			params Expression<Func<Product, object>>[] includeProperties)
		{
			var products = await _repository.GetFilterAndIncludeAsync(filter, includeProperties);
			return _mapper.Map<List<ProductDto>>(products);
		}

	}
}
