using AutoMapper;
using Demo.Domain;
using Demo.Dto;

namespace Demo.Application.AutoMapper
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<EditProductDto, Product>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Product, ProductDto>();
        }
    }

    public static class ProductMapper
    {
        static ProductMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Product ToEntity(this CreateProductDto dto)
        {
            return Mapper.Map<Product>(dto);
        }

        public static Product ToEntity(this EditProductDto dto, Product entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static ProductDto ToDto(this Product entity)
        {
            return Mapper.Map<ProductDto>(entity);
        }
    }
}
