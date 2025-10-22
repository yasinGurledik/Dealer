﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Domain.Entities;
public class Product
{
	public int Id { get; set; } 
	public string Name { get; set; } = null!;
	public decimal Price { get; set; }
	public int UserId { get; set; }
	public int CategoryId { get; set; }
	public Category? Category { get; set; }
}