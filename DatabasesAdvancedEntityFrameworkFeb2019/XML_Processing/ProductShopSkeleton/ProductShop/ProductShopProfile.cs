using AutoMapper;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserDto, User>();

            CreateMap<ProductDto, Product>();

            CreateMap<CategoryDto, Category>();

            CreateMap<CategoryProductDto, CategoryProduct>();

            CreateMap<ExportProductDto, Product>();



        }
    }
}
