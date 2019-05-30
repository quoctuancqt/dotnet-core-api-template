using AutoMapper;
using Demo.Domain;
using Demo.Dto;

namespace Demo.Application.AutoMapper
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<CreateCategoryDto, Category>();

            CreateMap<Category, CategoryDto>();
        }
    }

    public static class CategoryMapper
    {
        static CategoryMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<CategoryMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Category ToEntity(this CreateCategoryDto dto)
        {
            return Mapper.Map<Category>(dto);
        }

        public static CategoryDto ToDto(this Category entity)
        {
            return Mapper.Map<CategoryDto>(entity);
        }
    }
}
