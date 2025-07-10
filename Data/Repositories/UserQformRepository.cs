using Microsoft.EntityFrameworkCore;
using RetailxAPI.Data.Entities;
using RetailxAPI.Data.Models;

namespace RetailxAPI.Data.Repositories
{
    public class UserQformRepository
    {
        private readonly AppDbContext _context;

        public UserQformRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserQFormModel>> GetByQFormId(short qFormid)
        {
            var result = await _context.UserQform.Where(f => f.QformId == qFormid).ToListAsync();
            return result.Select(f => new UserQFormModel
            {
                UserId = f.UserId,
                QformId = f.QformId
            }).ToList();
        }
        public async Task<bool> AddUserQform(List<UserQFormModel> qForms)
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
                    _context.UserQform.RemoveRange(_context.UserQform.Where(uq => uq.QformId.HasValue && qformIds.Contains(uq.QformId.Value)));

                //    await _context.Database.ExecuteSqlRawAsync($"DELETE FROM UserQform WHERE QformId IN ({idList})");
                }

                // Ekle
                await _context.UserQform.AddRangeAsync(qForms.Select(q => new UserQform
                {
                    UserId = q.UserId,
                    QformId = q.QformId
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
