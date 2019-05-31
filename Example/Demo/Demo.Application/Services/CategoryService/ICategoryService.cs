using Demo.Dto;
using System.Threading.Tasks;

namespace Demo.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);

        Task<CategoryDto> GetByIdAsync(string id);
    }
}
