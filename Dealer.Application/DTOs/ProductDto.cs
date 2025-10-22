using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Application.DTOs;
public class ProductDto
{
	public int Id { get; set; } // update için
	public string Name { get; set; } = null!;
	public decimal Price { get; set; }
	public int CategoryId { get; set; }
	public string? CategoryName { get; set; }
}
