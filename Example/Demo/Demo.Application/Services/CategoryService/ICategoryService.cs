using Demo.Dto;
using System.Threading.Tasks;

namespace Demo.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);

        CategoryDto GetById(string id);
    }
}
