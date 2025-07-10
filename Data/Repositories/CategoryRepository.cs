using Microsoft.EntityFrameworkCore;
using RetailxAPI.Data.Entities;
using RetailxAPI.Data.Models;


namespace RetailxAPI.Data.Repositories
{
    public class CategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryModel>> GetCategories()
        {
            return await _context.Categories
                .Select(category => new CategoryModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                })
                .ToListAsync();
        }
        public async Task<CategoryModel> GetCategoryById(short id)
        {
            return await _context.Categories
                .Where(category => category.CategoryId == id)
                .Select(category => new CategoryModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                })
                .FirstOrDefaultAsync() ?? new CategoryModel();
        }
        public async Task<bool> AddCategory(CategoryModel categoryModel)
        {
            var newCategory = new Category
            {
                CategoryName = categoryModel.CategoryName
            };
            try
            {
                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateCategory(CategoryModel categoryModel)
        {
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == categoryModel.CategoryId);
            if (existingCategory == null)
            {
                return false;
            }
            existingCategory.CategoryName = categoryModel.CategoryName;
            try
            {
                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteCategory(short id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null)
            {
                return false;
            }
            try
            {
                _context.Categories.Remove(category);
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
