using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_PH35423.Data.Tables
{
    public class ExamHistoryDetails
    {
        [Key]
        public Guid IdExamHistoryDetail { get; set; }

        public Guid IdEH { get; set; }

        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Answer5 { get; set; }

        public int NumberRight { get; set; }

        public int NumberTrue { get; set; }

        [ForeignKey("IdEH")]
        public ExamHistory ExamHistory { get; set; }

    }
}
