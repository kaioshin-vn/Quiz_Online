using ASM_PH35423.Data.Tables;
using ASM_PH35423.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASM_PH35423.Data.DTO;
using ASM_PH35423.StaticClass;

namespace ASM_PH35423.Controllers
{
    public class MarkExamController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MarkExamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exams
        [HttpGet("/GetExam/{idExam}")]
        public async Task<List<QuestionTransferServer>> Index(Guid idExam)
        {
            var questions = new List<QuestionTransferServer>();
            if (await _context.Questions.CountAsync() > 0)
            {
                var listQuestions = _context.Questions.Where(a => a.IdExam == idExam).ToList();
                questions = listQuestions.Select(a =>
                {
                    var question = new QuestionTransferServer();
                    var count = 1;

                    question.ListAnswer.Add(new AnswerTransfer()
                    {
                        Value = count++,
                        Answer = a.Answer1
                    });
                    question.ListAnswer.Add(new AnswerTransfer()
                    {
                        Value = count++,
                        Answer = a.Answer2
                    });
                    question.ListAnswer.Add(new AnswerTransfer()
                    {
                        Value = count++,
                        Answer = a.Answer3
                    });
                    question.ListAnswer.Add(new AnswerTransfer()
                    {
                        Value = count++,
                        Answer = a.Answer4
                    });
                    question.ListAnswer.Add(new AnswerTransfer()
                    {
                        Value = count++,
                        Answer = a.Answer5
                    });

                    question.ListAnswer = question.ListAnswer.Where(a => a.Answer != "").ToList();
                    question.ListAnswer.Shuffle();


                    if (a.Img != "")
                    {
                        question.Img = a.Img;
                    }

                    question.Question = a.ContentQuestion;
                    question.IsMultiple = a.isMultiple;
                    question.Id = a.IdQuestion;

                    return question;
                }).ToList();

                questions.Shuffle();
            }

            return questions;
        }

        [HttpPost("/MarkExam")]
        public async Task<double> MarkExam([FromBody] List<QuestionTransferCustomer> listAnswer)
        {
            var listQuestion = new List<Question>();
            foreach (var item in listAnswer)
            {
                var question = await _context.Questions.FirstOrDefaultAsync(a => a.IdQuestion == item.Id);
                if (question != null)
                {
                    listQuestion.Add(question);
                }
            }


            var totalQuestion = listQuestion.Count;
            if (totalQuestion == 0)
            {
                return -1;
            }

            var totalTrueQuestion = 0;

            foreach (var item in listQuestion)
            {
                foreach (var answer in listAnswer)
                {
                    if (item.IdQuestion == answer.Id)
                    {
                        if (!item.isMultiple)
                        {
                            if (item.NumberRight == answer.NumbersAnwser)
                            {
                                totalTrueQuestion++;
                            }
                        }
                        else
                        {
                            var listAnswerRight = item.NumberRight.Split('_');
                            var listAnswerByCustom = answer.NumbersAnwser.Split('_');

                            var flagCheck = true;

                            foreach (var number in listAnswerByCustom)
                            {
                                if (listAnswerRight.Any(a => a == number))
                                {
                                    continue;
                                }
                                else
                                {
                                    flagCheck = false;
                                    break;
                                }
                            }

                            if (flagCheck && listAnswerByCustom.Count() == listAnswerRight.Count())
                            {
                                totalTrueQuestion++;
                            }
                        }
                    }
                }
            }


            var pointEachExam = 10 / Convert.ToDouble(totalQuestion);
            var pointExam = Math.Round(totalTrueQuestion * pointEachExam, 2);

            return pointExam;
        }

        [HttpPost("/CreateHistory")]
        public async Task<Guid> CreateHistory([FromBody] ExamHistory examHistory)
        {
            var id = Guid.NewGuid();
            examHistory.Id = id;
            await _context.ExamHistories.AddAsync(examHistory);
            _context.SaveChanges();
            return id;
        }

        [HttpPost("/CreateDetailHistory/{id}")]
        public async Task CreateHistoryDetail([FromBody] List<QuestionTransferCustomer> listAnswer , Guid id)
        {
            var listQuestion = new List<Question>();
            foreach (var item in listAnswer)
            {
                var question = await _context.Questions.FirstOrDefaultAsync(a => a.IdQuestion == item.Id);
                if (question != null)
                {
                    listQuestion.Add(question);
                }
            }

            var listHistoryDetail = new List<ExamHistoryDetails>();
            foreach (var item in listQuestion)
            {
                var ExamHD = new ExamHistoryDetails();
                ExamHD.IdEH = id;
                ExamHD.IdExamHistoryDetail = Guid.NewGuid();
                ExamHD.isMultiple = item.isMultiple;
                ExamHD.NumbersRight = item.NumberRight;
                ExamHD.NumbersChose = listAnswer.FirstOrDefault(a => a.Id == item.IdQuestion).NumbersAnwser;
                ExamHD.Question = item.ContentQuestion;

                ExamHD.Answer1 = item.Answer1;
                ExamHD.Answer2 = item.Answer2;
                if (item.Answer3 != "")
                {
                    ExamHD.Answer3 = item.Answer3;
                }
                if (item.Answer4 != "")
                {
                    ExamHD.Answer4 = item.Answer4;
                }
                if (item.Answer5 != "")
                {
                    ExamHD.Answer5 = item.Answer5;
                }
                if (item.Img != "")
                {
                    ExamHD.Img = item.Img;
                }

                listHistoryDetail.Add(ExamHD);
            }

            await _context.ExamHistoryDetails.AddRangeAsync(listHistoryDetail);
            _context.SaveChanges();
        }

        [HttpGet("/GetToTalQuestion/{idExam}")]
        public int GetToTalQuestion(Guid idExam)
        {
            return _context.Questions.Where(a => a.IdExam == idExam).Count();
        }

    }
}
