using Microsoft.EntityFrameworkCore;
using RetailxAPI.Data.Entities;
using RetailxAPI.Data.Models;

namespace RetailxAPI.Data.Repositories
{
    public class ShopRepository
    {
        private readonly AppDbContext _context;

        public ShopRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ShopModel>> GetShops()
        {
            return await _context.Shops
                .Select(shop => new ShopModel
                {
                    ShopId = shop.ShopId,
                    ShopName = shop.ShopName,
                    Latitude = shop.Latitude,
                    Longitude = shop.Longitude,
                    ShopPhone = shop.ShopPhone,
                    Address = shop.Address
                })
                .ToListAsync();
        }
        public async Task<ShopModel?> GetShopById(short shopId)
        {
            return await _context.Shops
                .Where(shop => shop.ShopId == shopId)
                .Select(shop => new ShopModel
                {
                    ShopId = shop.ShopId,
                    ShopName = shop.ShopName,
                    Latitude = shop.Latitude,
                    Longitude = shop.Longitude,
                    ShopPhone = shop.ShopPhone,
                    Address = shop.Address
                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> AddShop(ShopModel shopModel)
        {
            var shop = new Shop
            {
                ShopName = shopModel.ShopName,
                ShopPhone = shopModel.ShopPhone,
                Latitude = shopModel.Latitude,
                Longitude = shopModel.Longitude,
                Address = shopModel.Address
            };
            
            try
            {
                await _context.Shops.AddAsync(shop);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateShop(ShopModel shopModel)
        {
            var shop = await _context.Shops.FindAsync(shopModel.ShopId);
            if (shop == null)
            {
                return false;
            }
            shop.ShopName = shopModel.ShopName;
            shop.ShopPhone = shopModel.ShopPhone;
            shop.Latitude = shopModel.Latitude;
            shop.Longitude = shopModel.Longitude;
            shop.Address = shopModel.Address;
            try
            {
                _context.Shops.Update(shop);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteShop(short shopId)
        {
            var shop = await _context.Shops.FindAsync(shopId);
            if (shop == null)
            {
                return false;
            }
            try
            {
                _context.Shops.Remove(shop);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
