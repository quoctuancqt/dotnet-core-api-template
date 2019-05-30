using Demo.Application.Services;
using Demo.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.ApiServer.Controllers
{
    public class CategoryController : ApiBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);

            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult Get(string id)
        {
            return Ok(_categoryService.GetById(id));
        }
    }
}
