using AutoMapper;
using ProductsApp.DTO;
using ProductsApp.Models;

namespace ProductsApp.Config
{
	public class Mapper : Profile
	{
		public Mapper()
		{
			CreateMap<ProductInsertDTO, Product>().ReverseMap();
			CreateMap<ProductUpdateDTO, Product>().ReverseMap();
			CreateMap<ProductReadOnlyDTO, Product>().ReverseMap();
		}
	}
}
