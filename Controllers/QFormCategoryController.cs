using Microsoft.AspNetCore.Mvc;
using RetailxAPI.Data.Models;
using RetailxAPI.Data.Repositories;

namespace RetailxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QFormCategoryController : ControllerBase
    {
        private readonly QFormCategoryRepository _qFormCategoryRepository;

        public QFormCategoryController(QFormCategoryRepository qFormCategoryRepository)
        {
            _qFormCategoryRepository = qFormCategoryRepository;
        }
        [HttpGet("{qFormId}")]
        public async Task<IActionResult> GetCategoriesForQformsId(short qFormId)
        {
            var result = await _qFormCategoryRepository.GetByQFormId(qFormId);
            if (result == null || !result.Any())
            {
                return Ok(new List<QFormCategoryModel>());
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategoryToQform([FromBody] List<QFormCategoryModel> qForms)
        {
            if (qForms == null || !qForms.Any())
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var success = await _qFormCategoryRepository.AddQformCategory(qForms);
                if (!success)
                {
                    return StatusCode(500, "bilinmeyen hata");
                }
                return Ok("Kategori ve soru formu ilişkilendirme kaydı yapıldı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");

            }

        }
    }
}
