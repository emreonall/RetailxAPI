using Microsoft.EntityFrameworkCore;
using RetailxAPI.Data.Entities;
using RetailxAPI.Data.Models;

namespace RetailxAPI.Data.Repositories
{
    public class QFormCategoryRepository
    {
        private readonly AppDbContext _context;

        public QFormCategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<QFormCategoryModel>> GetByQFormId(short qFormid)
        {
            var result = await _context.QformCategory.Where(f => f.QformId == qFormid).ToListAsync();
            return result.Select(f => new QFormCategoryModel
            {
                CategoryId = f.CategoryId,
                QformId = f.QformId
            }).ToList();
        }
        public async Task<bool> AddQformCategory(List<QFormCategoryModel> qForms)
        {
            try
            {
                var qformIds = qForms
                    .Where(q => q.QformId != null)
                    .Select(q => q.QformId)
                    .Distinct()
                    .ToList();

                // Sil: SQL ile, PK olmadığı için
                if (qformIds.Any())
                {
                    var idList = string.Join(",", qformIds);
                  
                    //_context.QformCategories.RemoveRange(_context.QformCategories.Where(uq => uq.QformId.HasValue && qformIds.Contains(uq.QformId.Value)));

               
                    _context.QformCategory.RemoveRange(_context.QformCategory.Where(uq => qformIds.Contains(uq.QformId)));

                }

                // Ekle
                await _context.QformCategory.AddRangeAsync(qForms.Select(q => new QformCategory
                {
                   CategoryId=(short) q.CategoryId!,
                    QformId = (short)q.QformId!
                }));

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
