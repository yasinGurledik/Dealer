﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Domain.Entities;
public class Category
{
	public int Id { get; set; } 
	public string Name { get; set; } = null!;
	public ICollection<Product> Products { get; set; }  = default!;
}
