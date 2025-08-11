using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailxAPI.Data.Models;
using RetailxAPI.Data.Repositories;

namespace RetailxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryQuestionsController : ControllerBase
    {
        private readonly CategoryQuestionsRepository _categoryQuestionsRepository;

        public CategoryQuestionsController(CategoryQuestionsRepository categoryQuestionsRepository)
        {
            _categoryQuestionsRepository = categoryQuestionsRepository;
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryQuestions(short categoryId)
        {
            var questions = await _categoryQuestionsRepository.GetCategoryQuestions(categoryId);
            if (questions == null || !questions.Any())
            {
                return NotFound();
            }
            return Ok(questions);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoryQuestion([FromBody] List<CategoryQuestionsModel> categoryQuestions)
        {
            if (categoryQuestions == null || !categoryQuestions.Any())
            {
                return BadRequest("Kayıt listesi boş olamaz.");
            }
            var result= await _categoryQuestionsRepository.CreateCategoryQuestion(categoryQuestions);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Kayıt yapılamadı.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryQuestion([FromBody] List<CategoryQuestionsModel> categoryQuestions)
        {
            if (categoryQuestions == null || !categoryQuestions.Any())
            {
                return BadRequest("Kayıt listesi boş olamaz.");
            }
            var result = await _categoryQuestionsRepository.UpdateCategoryQuestion(categoryQuestions);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Kayıt güncellenemedi.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryQuestion([FromBody] List<CategoryQuestionsModel> categoryQuestions)
        {
            if (categoryQuestions == null || !categoryQuestions.Any())
            {
                return BadRequest("Kayıt listesi boş olamaz.");
            }
            var result = await _categoryQuestionsRepository.DeleteCategoryQuestion(categoryQuestions);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Kayıt silinemedi.");
        }
        [HttpPost("CreateSingleQuestion")]
        public async Task<IActionResult> CreateCategoryQuestion([FromBody] CategoryQuestionsModel categoryQuestion)
        {
            if (categoryQuestion == null)
            {
                return BadRequest("Soru bilgileri boş olamaz.");
            }
            var result = await _categoryQuestionsRepository.CreateSingleQuestion(categoryQuestion);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Kayıt yapılamadı.");
        }
        [HttpDelete("DeleteSingleQuestion")]
        public async Task<IActionResult> DeleteCategoryQuestion([FromBody] CategoryQuestionsModel categoryQuestion)
        {
            if (categoryQuestion == null)
            {
                return BadRequest("Silinecek soru bilgisi boş olamaz.");
            }
            var result = await _categoryQuestionsRepository.DeleteSingleQuestion(categoryQuestion);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Kayıt silinemedi.");
        }
        [HttpPut("UpdateSingleQuestion")]
        public async Task<IActionResult> UpdateCategoryQuestion([FromBody] CategoryQuestionsModel categoryQuestion)
        {
            if (categoryQuestion == null )
            {
                return BadRequest("Güncellenecek soru bilgileri boş olamaz.");
            }
            var result = await _categoryQuestionsRepository.UpdateSingleQuestion(categoryQuestion);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Kayıt güncellenemedi.");
        }
    }
}
