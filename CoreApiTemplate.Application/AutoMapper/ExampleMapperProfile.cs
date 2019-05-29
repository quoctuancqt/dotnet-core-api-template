using AutoMapper;

namespace CoreApiTemplate.Application.AutoMapper
{
    public class ExampleMapperProfile : Profile
    {
        public ExampleMapperProfile()
        {
            //CreateMap<TSource, TDestination>();
        }
    }

    public static class ExampleMapper
    {
        static ExampleMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ExampleMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        //public static TDestination ToEntity(this TSource dto)
        //{
        //    return Mapper.Map<TDestination>(dto);
        //}
    }
}
