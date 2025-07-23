using Microsoft.AspNetCore.Mvc;
using RetailxAPI.Data.Models;
using RetailxAPI.Data.Repositories;

namespace RetailxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UserController>  
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userList = await _userRepository.GetUsers();
            if (userList==null)
            {
                return NotFound();
            }
            return Ok(userList);
        }

        // GET api/<UserController>/5  
        [HttpGet("id/{id}")]
        public async Task<IActionResult> Get(short id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("name/{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            var user = await _userRepository.GetUserByUserName(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UserController>  
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserModel userModel)
        {
            if (userModel == null)
            {
                return BadRequest("Kullanıcı bilgileri boş olamaz.");
            }
            var result = await _userRepository.AddUser(userModel);
            if (result)
            {
                return CreatedAtAction(nameof(Get), new { id = userModel.UserId }, userModel);
            }
            return BadRequest("Kullanıcı yaratılırken bir hata oluştu.");
        }

        // PUT api/<UserController>/5  
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, [FromBody] UserModel userModel)
        {
            if (userModel == null || userModel.UserId != id)
            {
                return BadRequest("Kullanıcı bilgileri geçersiz.");
            }
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            var result = await _userRepository.UpdateUser(userModel);
            if (result)
            {
                return Ok(new { message = "Kullanıcı başarıyla güncellendi." });
            }
            return BadRequest("Kullanıcı güncellenirken bir hata oluştu.");
        }
      

        // DELETE api/<UserController>/5  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            var result = await _userRepository.DeleteUser(id);
            if (result)
            {
                return Ok(new { message = "Kullanıcı başarıyla silindi." });
            }
            return BadRequest("Kullanıcı silinirken hata oluştu");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Passwd))
            {
                return BadRequest("Kullanıcı adı ve şifre boş olamaz.");
            }
            var user = await _userRepository.AdminUserLogin(loginModel);
            if (user)
            {
                return Ok(user);
            }
            return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
        }
    }
}
