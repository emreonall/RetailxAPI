using Microsoft.AspNetCore.Mvc;
using RetailxAPI.Data.Models;
using RetailxAPI.Data.Repositories;

namespace RetailxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCategoryById(short id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryModel categoryModel)
        {
            if (categoryModel == null)
            {
                return BadRequest("Kategori bilgileri boş olamaz.");
            }
            var result = await _categoryRepository.AddCategory(categoryModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Kategori eklenirken bir hata oluştu.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(short id, [FromBody] CategoryModel categoryModel)
        {
            if (categoryModel == null || categoryModel.CategoryId != id)
            {
                return BadRequest("Kategori bilgileri geçersiz.");
            }
            var result = await _categoryRepository.UpdateCategory(categoryModel);
            if (result)
            {
                return Ok();
            }
            return NotFound("Kategori bulunamadı veya güncelleme başarısız oldu.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(short id)
        {
            var result = await _categoryRepository.DeleteCategory(id);
            if (result)
            {
                return Ok(new { message = "Kategori başarıyla silindi." });
            }
            return NotFound("Kategori bulunamadı veya silme başarısız oldu.");
        }
    }
}