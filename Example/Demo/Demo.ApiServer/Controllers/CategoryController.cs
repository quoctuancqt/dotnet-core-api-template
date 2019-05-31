using Demo.Application.Services;
using Demo.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.ApiServer.Controllers
{
    [Authorize("AdminPolicy")]
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
            return Ok(await _categoryService.CreateAsync(dto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _categoryService.GetByIdAsync(id));
        }
    }
}
