using Microsoft.EntityFrameworkCore;
using RetailxAPI.Data.Entities;
using RetailxAPI.Data.Models;

namespace RetailxAPI.Data.Repositories
{
    public class QFormRepository
    {
        private readonly AppDbContext _context;

        public QFormRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<QFormModel>> GetQForms()
        {
            return await _context.Qforms
                .Select(qForm => new QFormModel
                {
                    QformId = qForm.QformId,
                    QformName = qForm.QformName
                })
                .ToListAsync();
        }
        public async Task<QFormModel> GetQFormById(short id)
        {
            return await _context.Qforms
                .Where(qForm => qForm.QformId == id)
                .Select(qForm => new QFormModel
                {
                    QformId = qForm.QformId,
                    QformName = qForm.QformName
                })
                .FirstOrDefaultAsync() ?? new QFormModel();
        }
        public async Task<bool> AddQForm(QFormModel qFormModel)
        {
            var newQForm = new Qform
            {
                QformName = qFormModel.QformName
            };
            try
            {
                await _context.Qforms.AddAsync(newQForm);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateQForm(QFormModel qFormModel)
        {
            var existingQForm = await _context.Qforms
                .FirstOrDefaultAsync(q => q.QformId == qFormModel.QformId);
            if (existingQForm == null)
            {
                return false;
            }
            existingQForm.QformName = qFormModel.QformName;
            try
            {
                _context.Qforms.Update(existingQForm);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteQForm(short id)
        {
            var qForm = await _context.Qforms.FindAsync(id);
            if (qForm == null)
            {
                return false;
            }
            try
            {
                _context.Qforms.Remove(qForm);
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
