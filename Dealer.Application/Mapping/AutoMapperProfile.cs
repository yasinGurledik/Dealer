using AutoMapper;
using Dealer.Application.DTOs;
using Dealer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dealer.Application.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
				.ReverseMap();
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<User, UserDto>().ReverseMap();
		}
	}
}
