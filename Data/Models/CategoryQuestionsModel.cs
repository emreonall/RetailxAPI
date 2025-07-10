using System.ComponentModel.DataAnnotations;

namespace RetailxAPI.Data.Models
{
    public class CategoryQuestionsModel
    {
        public short CategoryID { get; set; }
        public short RowOrder { get; set; }
        public string Question { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty;
        public string Answer1 { get; set; } = string.Empty;
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? Answer5 { get; set; }
    }
}
