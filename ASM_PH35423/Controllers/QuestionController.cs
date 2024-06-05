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
        [HttpGet("/GetQuestions/{idExam}")]
        public async Task<List<Question>> Index(Guid idExam)
        {
            var questions = new List<Question>();
            if (await _context.Questions.CountAsync() > 0)
            {
                questions = _context.Questions.Where(a => a.IdExam == idExam).ToList();
            }
            return questions;
        }

        [HttpPost("/CreateQuestions")]
        public async Task Create([FromBody] Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        [HttpGet("/GetDetailQuestion/{idDetail}")]
        public async Task<Question?> GetExams(Guid idDetail)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(a => a.IdQuestion == idDetail);

            return question;
        }

        [HttpPatch("/UpdateQuestion")]
        public async Task Update([FromBody] Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }


        [HttpDelete("/DeleteQuestion/{idDetail}")]
        public async Task<bool> Delete(Guid idDetail)
        {
            var question =await _context.Questions.FindAsync(idDetail);
            _context.Questions.Remove(question);

            return _context.SaveChanges()>0;
        } 
    }
}
