using ASM_PH35423.Data.Tables;

namespace ASM_PH35423.Data.DTO
{
    public class ExamResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Img { get; set; }

        public DateTime DateCreate { get; set; }
        public int PeopleCount { get; set; }

    }

    public class ExamResultDetail
    {
        public Guid Id { get; set; }
        public string NameUser { get; set; }

        public double Point { get; set; } 

        public double TimeDoExam { get; set; }

        public DateTime Date { get; set; }

        public string? Code { get; set; }
    }

    public class ExamResultHasCode
    {
        public string Code { get; set; }

        public List<ExamHistory> ListExamHis { get; set; } = new List<ExamHistory> ();
    }
}
