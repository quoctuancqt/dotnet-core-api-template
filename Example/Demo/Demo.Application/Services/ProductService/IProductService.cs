using Demo.Dto;
using System.Threading.Tasks;

namespace Demo.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(CreateProductDto dto);
    }
}
