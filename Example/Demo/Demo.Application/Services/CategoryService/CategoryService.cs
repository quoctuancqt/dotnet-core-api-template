using System.Threading.Tasks;
using Demo.Application.AutoMapper;
using Demo.Core.Exceptions;
using Demo.Core.Repository;
using Demo.Domain;
using Demo.Dto;

namespace Demo.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var entity = dto.ToEntity();

            _repository.Add(entity);

            await _repository.UnitOfWork.SaveChangeAsync();

            return entity.ToDto();
        }

        public CategoryDto GetById(string id)
        {
            var entity = _repository.GetById(id);

            if (entity == null) throw new BadRequestException("Not found.");

            return entity.ToDto();
        }
    }
}
