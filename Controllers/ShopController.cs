using RetailxAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using RetailxAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;

using RetailxAPI.Data.Entities;
using ClosedXML.Excel;

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
                return Ok(new List<ShopModel>());
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
                return NotFound("Kayıt bulunamadı...");
            }
            return Ok(shop);
        }
        // POST api/<ShopController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShopModel shopModel)
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
        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya yüklenmedi.");

            var shops = new List<Shop>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using var workbook = new XLWorkbook(stream);

                var worksheet = workbook.Worksheet(1);

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var shop = new Shop
                    {
                        ShopName = row.Cell(1).GetString().Trim(),
                        ShopPhone = row.Cell(2).GetString().Trim(),
                        Latitude = TryGetDecimal(row.Cell(3).GetValue<string>()),
                        Longitude = TryGetDecimal(row.Cell(4).GetValue<string>()),
                        Address = row.Cell(5).GetString().Trim()
                    };
                    shops.Add(shop);
                }
            }

            var result = await _shopRepository.AddRange(shops);

            if (!result)
            {
                return BadRequest("Mağazalar eklenirken bir hata oluştu.");
            }
            return Ok(new { Success = true, Count = shops.Count });
        }
        private decimal? TryGetDecimal(string? value)
        {
            if (decimal.TryParse(value, System.Globalization.NumberStyles.Any,
                                 System.Globalization.CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return null;
        }
    }
}
