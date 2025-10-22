using Dealer.Application.DTOs;
using Dealer.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dealer.API.Controllers
{
	[Route("api/[controller]/action")]
	[ApiController]
	[Authorize]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _service;

		public ProductController(IProductService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var products = await _service.GetAllAsync();
			return Ok(products);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProductWithCategory()
		{
			var products = await _service.GetFilterAndIncludeAsync(null, p => p.Category);
			return Ok(products);
		}
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await _service.GetByIdAsync(id);
			if (product == null) return NotFound();
			return Ok(product);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ProductDto dto)
		{
			var created = await _service.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
		{
			await _service.UpdateAsync(id, dto);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _service.DeleteAsync(id);
			return NoContent();
		}
	}
}
