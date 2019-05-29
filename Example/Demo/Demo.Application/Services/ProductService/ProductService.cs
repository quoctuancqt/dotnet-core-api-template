using System.Threading.Tasks;
using Demo.Application.AutoMapper;
using Demo.Core.Repository;
using Demo.Domain;
using Demo.Dto;

namespace Demo.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var entity = dto.ToEntity();

            _productRepository.Add(entity);

            await _productRepository.UnitOfWork.SaveChangesAsync();

            return entity.ToDto();
        }
    }
}
