using System.Threading.Tasks;
using Demo.Application.AutoMapper;
using Demo.Application.Repositories;
using Demo.Core.Repository;
using Demo.Domain;
using Demo.Dto;

namespace Demo.Application.Services
{
    public class CategoryService : ICategoryService
    {
        //private readonly ICategoryRepository _categoryRepository;
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository
        //,ICategoryRepository categoryRepository
        )
        {
            //_categoryRepository = categoryRepository;
            _repository = repository;
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var entity = dto.ToEntity();

            //await _categoryRepository.CreateAsync(entity);
            _repository.Add(entity);

            await _repository.UnitOfWork.SaveChangesAsync();

            return entity.ToDto();
        }

        public async Task<CategoryDto> GetByIdAsync(string id)
        {
            //var entity = await _categoryRepository.GetByIdAsync(id);
            var entity = await _repository.GetByIdAsync(id);

            return entity.ToDto();
        }
    }
}
