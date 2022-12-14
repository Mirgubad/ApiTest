using Business.DTOs.Category.Request;
using Business.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region Documentation
        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns> 
        #endregion
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        #region Documentation
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
         #endregion
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _categoryService.GetAsync(id));
        }
        #region Documentation
        /// <summary>
        /// Search by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        #endregion
        [HttpGet("filterbyname")]
        public async Task<IActionResult> FilterByName(string? name)
        {
            return Ok(await _categoryService.FilterByName(name));
        }

        #region Documentation
        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        #endregion
        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryCreateDTO model)
        {
            return Ok(await _categoryService.CreateAsync(model));

        }

        #region Documentation
        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        #endregion
        [HttpPost("update")]
        public async Task<IActionResult> Update(CategoryUpdateDTO model)
        {
            return Ok(await _categoryService.UpdateAsync(model));
        }


        #region Documentation
        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #endregion
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _categoryService.DeleteAsync(id));
        }



    }
}
