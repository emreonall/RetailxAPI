using RetailxAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using RetailxAPI.Data.Repositories;

namespace RetailxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ShopRepository _shopRepository;

        public ShopController(ShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        // GET: api/<ShopController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shopList = await _shopRepository.GetShops();
            if (shopList == null)
            {
                return NotFound();
            }
            return Ok(shopList);
        }
        // GET api/<ShopController>/5
        [HttpGet("id/{id}")]
        public async Task<IActionResult> Get(short id)
        {
            var shop = await _shopRepository.GetShopById(id);
            if (shop == null)
            {
                return NotFound();
            }
            return Ok(shop);
        }
        // POST api/<ShopController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ShopModel shopModel)
        {
            if (shopModel == null)
            {
                return BadRequest("Mağaza bilgileri boş olamaz.");
            }
            var result = await _shopRepository.AddShop(shopModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Mağaza eklenirken bir hata oluştu.");
        }
        // PUT api/<ShopController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, [FromBody] Data.Models.ShopModel shopModel)
        {
            if (shopModel == null || shopModel.ShopId != id)
            {
                return BadRequest("Mağaza bilgileri geçersiz.");
            }
            var existingShop = await _shopRepository.GetShopById(id);
            if (existingShop == null)
            {
                return NotFound();
            }
            var result = await _shopRepository.UpdateShop(shopModel);
            if (result)
            {
                return Ok(new { message = "Mağaza bilgileri güncellendi." });
            }
            return BadRequest("Mağaza bilgileri güncellenirken bir hata oluştu.");
        }
        // DELETE api/<ShopController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var existingShop = await _shopRepository.GetShopById(id);
            if (existingShop == null)
            {
                return NotFound();
            }
            var result = await _shopRepository.DeleteShop(id);
            if (result)
            {
                return Ok(new { message = "Mağaza başarıyla silindi." });
            }
            return BadRequest("Mağaza silinirken bir hata oluştu.");
        }
    }
}
