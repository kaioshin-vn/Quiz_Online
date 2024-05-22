
using Microsoft.AspNetCore.Mvc;
using ASM_PH35423.Data;
using ASM_PH35423.Data.Tables;
using Microsoft.EntityFrameworkCore;


namespace ASM_PH35423.Controllers
{
    [ApiController]
    [Route("/Exams")]
    public class ExamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exams
        [HttpGet("/GetExams/{idUser}")]
        public async Task<List<Exam>> Index(Guid idUser)
        {
            var exams = _context.Exams.Where(a => a.IdUser == idUser &&  a.IsDeleted == false).ToList();
            return exams;
        }

        [HttpGet("/GetExamDetail/{idDetail}")]
        public async Task<Exam?> GetExams(Guid idDetail)
        {
            var exam = await _context.Exams.FirstOrDefaultAsync(a => a.Id == idDetail);

            return exam;
        }

        [HttpPatch("/UpdateExam")]
        public async Task Update([FromBody] Exam exam)
        {
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
        }

        [HttpDelete("/DeleteExam/{idExam}")]
        public async Task DeleteExam( Guid idExam)
        {
            var exam = _context.Exams.Find(idExam);
            exam.IsDeleted = true;
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
        }

        [HttpPost("/AddExam")]
        public async Task<Guid> Create([FromBody] Guid idUser)
        {
            
            
            var Exam = new Exam();
            var id = Guid.NewGuid();
            Exam.Id = id;
            Exam.Img = "/Img/bird-thumbnail.jpg";
            Exam.Mode = Data.Enums.ModeExam.Free;
            Exam.CreateDate = DateTime.Now;
            Exam.Descripton = "N/A";
            Exam.Type = "N/A";
            Exam.Title = "Bài quiz chưa có tiêu đề";
            Exam.IdUser = idUser;


            await _context.Exams.AddAsync(Exam);
            await _context.SaveChangesAsync();
            return id;
        }

        // GET: Exams/Edit/5
     
    }
}
