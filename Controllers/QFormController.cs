using Microsoft.AspNetCore.Mvc;
using RetailxAPI.Data.Models;
using RetailxAPI.Data.Repositories;

namespace RetailxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QFormController : ControllerBase
    {
        private readonly QFormRepository _qFormRepository;

        public QFormController(QFormRepository qFormRepository)
        {
            _qFormRepository = qFormRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetQForms()
        {
            var qForms = await _qFormRepository.GetQForms();
            if (qForms == null)
            {
                return NotFound();
            }
            return Ok(qForms);
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetQFormById(short id)
        {
            var qForm = await _qFormRepository.GetQFormById(id);
            if (qForm == null)
            {
                return NotFound();
            }
            return Ok(qForm);
        }
        [HttpPost]
        public async Task<IActionResult> AddQForm([FromBody] QFormModel qFormModel)
        {
            if (qFormModel == null)
            {
                return BadRequest("Soru Formu bilgileri boş olamaz.");
            }
            var result = await _qFormRepository.AddQForm(qFormModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Soru formu eklenirken bir hata oluştu.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQForm(short id, [FromBody] QFormModel qFormModel)
        {
            if (qFormModel == null || qFormModel.QformId != id)
            {
                return BadRequest("Soru Formu bilgileri geçersiz.");
            }
            var result = await _qFormRepository.UpdateQForm(qFormModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Soru formu güncellenirken bir hata oluştu.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQForm(short id)
        {
            var result = await _qFormRepository.DeleteQForm(id);
            if (result)
            {
                return Ok();
            }
            return NotFound("Soru formu bulunamadı veya silme işlemi başarısız oldu.");
        }
    }
}
