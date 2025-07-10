using Microsoft.AspNetCore.Mvc;
using RetailxAPI.Data.Models;
using RetailxAPI.Data.Repositories;

namespace RetailxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQformController : ControllerBase
    {
        private readonly UserQformRepository _userQformRepository;

        public UserQformController(UserQformRepository userQformRepository)
        {
            _userQformRepository = userQformRepository;
        }

        [HttpGet("{qFormId}")]
        public async Task<IActionResult> GetUsersForQformsId(short qFormId)
        {
            var result = await _userQformRepository.GetByQFormId(qFormId);
            if (result == null || !result.Any())
            {
                return NotFound("No users found for the specified QForm ID.");
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserQform([FromBody] List<UserQFormModel> qForms)
        {
            if (qForms == null || !qForms.Any())
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var success = await _userQformRepository.AddUserQform(qForms);
                if (!success)
                {
                    return StatusCode(500, "bilinmeyen hata");
                }
                return Ok("Kullanıcı ve soru formu ilişkilendirme kaydı yapıldı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
             
            }
           
        }

    }
}
