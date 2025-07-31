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
        public short Answer1Puan { get; set; } = 0;
        public short Answer2Puan { get; set; } = 0;   
        public short Answer3Puan { get; set; } = 0;
        public short Answer4Puan { get; set; } = 0;
        public short Answer5Puan { get; set; } = 0;
    }
}
