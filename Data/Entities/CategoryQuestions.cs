using System.ComponentModel.DataAnnotations;

namespace RetailxAPI.Data.Entities
{
    public class CategoryQuestions
    {
        public short CategoryID { get; set; }
        public short RowOrder { get; set; }
        public string Question { get; set; }=string.Empty;
        [MaxLength(20)]
        public string QuestionType { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Answer1 { get; set; } = string.Empty;
        [MaxLength(150)]
        public string? Answer2 { get; set; }
        [MaxLength(150)]
        public string? Answer3 { get; set; }
        [MaxLength(150)]
        public string? Answer4 { get; set; }
        [MaxLength(150)]
        public string? Answer5 { get; set; }
    }
}
