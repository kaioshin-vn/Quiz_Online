
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
            var exams = new List<Exam>();

            if (_context.Exams.Count()>0)
            {
                 exams = await _context.Exams.Where(a => a.IdUser == idUser && a.IsDeleted == false).ToListAsync();
            }
            return exams;
        }

        [HttpGet("/GetAllExams")]
        public async Task<List<Exam>> Index()
        {
            var exams = new List<Exam>();

            if (_context.Exams.Count() > 0)
            {
                exams = await _context.Exams.Where(a =>a.IsDeleted == false).ToListAsync();
            }
            return exams;
        }


        [HttpPatch("/UpdateCodeExam/{id}")]
        public async Task<IActionResult> UpdateCode(Guid id ,[FromBody] string code)
        {

            if (_context.Exams.Count() > 0)
            {
                var exam = await _context.Exams.FirstOrDefaultAsync(a => a.Id == id);
                exam.Code = code;
                _context.Exams.Update(exam);
                _context.SaveChanges();
                await Console.Out.WriteLineAsync("cc\nccc");
            }
            return Ok();
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
            Exam.CreateDate = DateTime.Now;
            Exam.Descripton = "N/A";
            Exam.Type = "N/A";
            Exam.Title = "Bài quiz chưa có tiêu đề";
            Exam.IdUser = idUser;
            Exam.Time = 1;

            await _context.Exams.AddAsync(Exam);
            await _context.SaveChangesAsync();
            return id;
        }

        [HttpGet("/GetToExamByCode/{code}")]
        public async Task<string?> GetExamByCode( string code)
        {
            var exam = await _context.Exams.FirstOrDefaultAsync(a => a.Code == code && a.IsDeleted == false);
            if (exam != null)
            {
                return exam.Id.ToString();
            }
            else
            {
                return "";
            }
        }

        // GET: Exams/Edit/5

    }
}
