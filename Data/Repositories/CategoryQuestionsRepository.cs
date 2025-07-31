using Microsoft.EntityFrameworkCore;
using RetailxAPI.Data.Entities;
using RetailxAPI.Data.Models;

namespace RetailxAPI.Data.Repositories
{
    public class CategoryQuestionsRepository
    {
        private readonly AppDbContext _context;

        public CategoryQuestionsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryQuestionsModel>> GetCategoryQuestions(short categoryId)
        {
            return await _context.CategoryQuestions
                .Where(q => q.CategoryID == categoryId)
                .Select(q => new CategoryQuestionsModel
                {
                    CategoryID = q.CategoryID,
                    RowOrder = q.RowOrder,
                    Question = q.Question,
                    QuestionType = q.QuestionType,
                    Answer1 = q.Answer1,
                    Answer2 = q.Answer2,
                    Answer3 = q.Answer3,
                    Answer4 = q.Answer4,
                    Answer5 = q.Answer5,
                    Answer1Puan = q.Answer1Puan,
                    Answer2Puan = q.Answer2Puan,
                    Answer3Puan = q.Answer3Puan,
                    Answer4Puan = q.Answer4Puan,
                    Answer5Puan = q.Answer5Puan
                })
                .ToListAsync();
        }
        public async Task<bool> CreateCategoryQuestion(List<CategoryQuestionsModel> categoryQuestions)
        {
            try
            {
                var entities = categoryQuestions.Select(q => new CategoryQuestions
                {
                    CategoryID = q.CategoryID,
                    RowOrder = q.RowOrder,
                    QuestionType = q.QuestionType,
                    Question = q.Question,
                    Answer1 = q.Answer1,
                    Answer2 = q.Answer2,
                    Answer3 = q.Answer3,
                    Answer4 = q.Answer4,
                    Answer5 = q.Answer5,
                    Answer1Puan = q.Answer1Puan,
                    Answer2Puan = q.Answer2Puan,
                    Answer3Puan = q.Answer3Puan,
                    Answer4Puan = q.Answer4Puan,
                    Answer5Puan = q.Answer5Puan
                }).ToList();

                await _context.CategoryQuestions.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> UpdateCategoryQuestion(List<CategoryQuestionsModel> categoryQuestions)
        {
            var catIds = categoryQuestions.Select(q => q.CategoryID).Distinct();
            var rowOrders = categoryQuestions.Select(q => q.RowOrder).Distinct();

            var existing = await _context.CategoryQuestions
                .Where(q => catIds.Contains(q.CategoryID) && rowOrders.Contains(q.RowOrder))
                .ToListAsync();

            foreach (var item in categoryQuestions)
            {
                var match = existing.FirstOrDefault(q => q.CategoryID == item.CategoryID && q.RowOrder == item.RowOrder);
                if (match != null)
                {
                    match.Question = item.Question;
                    match.QuestionType = item.QuestionType;
                    match.Answer1 = item.Answer1;
                    match.Answer2 = item.Answer2;
                    match.Answer3 = item.Answer3;
                    match.Answer4 = item.Answer4;
                    match.Answer5 = item.Answer5;
                    match.Answer1Puan = item.Answer1Puan;
                    match.Answer2Puan = item.Answer2Puan;
                    match.Answer3Puan = item.Answer3Puan;
                    match.Answer4Puan = item.Answer4Puan;
                    match.Answer5Puan = item.Answer5Puan;

                }
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteCategoryQuestion(List<CategoryQuestionsModel> categoryQuestions)
        {
            var categoryIds = categoryQuestions.Select(x => x.CategoryID).Distinct().ToList();
            var rowOrders = categoryQuestions.Select(x => x.RowOrder).Distinct().ToList();

            var allToCheck = await _context.CategoryQuestions
                .Where(q => categoryIds.Contains(q.CategoryID) && rowOrders.Contains(q.RowOrder))
                .ToListAsync();

            var toDelete = allToCheck
                .Where(q => categoryQuestions.Any(m => m.CategoryID == q.CategoryID && m.RowOrder == q.RowOrder))
                .ToList();
            try
            {
                _context.CategoryQuestions.RemoveRange(toDelete);
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
