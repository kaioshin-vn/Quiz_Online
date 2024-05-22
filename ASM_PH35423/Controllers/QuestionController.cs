using ASM_PH35423.Data.Tables;
using ASM_PH35423.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM_PH35423.Controllers
{
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exams
        [HttpGet("/GetExams/{idUser}")]
        public async Task<List<Exam>> Index(Guid idUser)
        {
            var exams = _context.Exams.Where(a => a.IdUser == idUser && a.IsDeleted == false).ToList();
            return exams;
        }

        [HttpGet("/GetExamDetail/{idDetail}")]
        public async Task<Exam?> GetExams(Guid idDetail)
        {
            var exam = await _context.Exams.FirstOrDefaultAsync(a => a.Id == idDetail);

            return exam;
        }
    }
}
