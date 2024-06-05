using ASM_PH35423.Data;
using ASM_PH35423.Data.DTO;
using ASM_PH35423.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM_PH35423.Controllers
{
    [ApiController]
    public class HistoryExamController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoryExamController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/GetExamHistory/{id}")]
        public ExamHistory? GetExamHistory(Guid id)
        {
            var ExamHistory = _context.ExamHistories.FirstOrDefault(a => a.Id == id);
            return ExamHistory;
        }

        [HttpGet("/GetListResultExam/{id}")]
        public List<ExamResult>? GetListResultExam(Guid id)
        {
            var listExamResult = new List<ExamResult>();
            listExamResult = _context.Exams.Where(a => a.IdUser == id && a.IsDeleted == false ).ToList().Select(b =>
            {
                var examResult = new ExamResult();
                examResult.Id = b.Id;
                examResult.Title = b.Title;
                examResult.DateCreate = b.CreateDate;
                examResult.Img = b.Img;
                examResult.PeopleCount = _context.ExamHistories.Count(a => a.IdExam == a.Id);

                return examResult;
            }).ToList();



            return listExamResult;
        }

        [HttpGet("/GetExamResultNoCode/{id}")]
        public List<ExamResultDetail>? GetResultExam(Guid id)
        {
            var listExamResult = new List<ExamResultDetail>();
            var listExamHistory = _context.ExamHistories.Where( a => a.IdExam == id ).ToList();

            foreach (var item in listExamHistory.ToList())
            {
                if (item.Info.Split("_").Count() != 1)
                {
                    listExamHistory.Remove(item);
                }
            }

            foreach (var item in listExamHistory)
            {
                var ExamHistoryDetails = new ExamResultDetail();
                var timeDoExam = (item.TimeEnd - item.TimeStart).TotalMinutes;

                ExamHistoryDetails.TimeDoExam = timeDoExam;
                ExamHistoryDetails.Date = item.TimeStart;
                ExamHistoryDetails.Point = item.Scores;

                var userName = _context.Users.FirstOrDefault(a => a.Id == item.IdUser).UserName;
                ExamHistoryDetails.NameUser = userName;
                ExamHistoryDetails.Id = item.Id;
                listExamResult.Add(ExamHistoryDetails);
            }

            return listExamResult;
        }

        [HttpGet("/GetExamResultHasCode/{id}")]
        public List<ExamResultHasCode>? GetResultExamHasCode(Guid id)
        {
            var listExamResult = new List<ExamResultHasCode>();
            var listExamHistory = _context.ExamHistories.Include(a => a.User).Where(a => a.IdExam == id).ToList();
            foreach (var item in listExamHistory)
            {
                if (item.Info.Split("_").Count() != 2)
                {
                    listExamHistory.Remove(item);
                }
                else
                {
                    item.Info = item.Info.Split('_')[1];
                }
            }
            
            var listExamHistoryGrouped = listExamHistory.GroupBy(a => a.Info).ToList();

            foreach (var listExamHistoryHasCode in listExamHistoryGrouped)
            {
                var ExamHasCode = new ExamResultHasCode();
                ExamHasCode.Code = listExamHistoryHasCode.Key;
                ExamHasCode.ListExamHis = listExamHistoryHasCode.ToList();
                listExamResult.Add(ExamHasCode);
            }

            return listExamResult;
        }



        [HttpGet("/GetListHistoryUser/{id}")]
        public List<ExamHistory>? GetHistoryUser(Guid id)
        {
            var listHistory = new List<ExamHistory>();
            listHistory = _context.ExamHistories.Where(a => a.IdUser == id).ToList();
            return listHistory;
        }

        [HttpGet("/GetListExamHistory/{id}")]
        public List<HistoryTransfer>? GetListExamHistory(Guid id)
        {
            var ListExamHistory = _context.ExamHistoryDetails.Where(a => a.IdEH == id).ToList();
            var listHistory = new List<HistoryTransfer>();
            listHistory = ListExamHistory.Select ( a =>
            {
                var history = new HistoryTransfer();

                var anwserCustom = a.NumbersChose;
                var resolve = a.NumbersRight;
                if (a.isMultiple)
                {
                    anwserCustom = ArrangeString(a.NumbersChose);
                    resolve = ArrangeString(a.NumbersRight);
                }
                

                history.Question = a.Question;
                history.AnwserCustom = anwserCustom;
                history.Resolve = resolve;
                history.IsMultiple = a.isMultiple;
                history.ListAnswer = new List<AnswerTransfer>();
                if (a.Img != null)
                {
                    history.Img = a.Img;
                }

                var count = 1;

                history.ListAnswer.Add(new AnswerTransfer()
                {
                    Value = count++,
                    Answer = a.Answer1
                });

                history.ListAnswer.Add(new AnswerTransfer()
                {
                    Value = count++,
                    Answer = a.Answer2
                });

                history.ListAnswer.Add(new AnswerTransfer()
                {
                    Value = count++,
                    Answer = a.Answer3
                });

                history.ListAnswer.Add(new AnswerTransfer()
                {
                    Value = count++,
                    Answer = a.Answer4
                });

                history.ListAnswer.Add(new AnswerTransfer()
                {
                    Value = count++,
                    Answer = a.Answer5
                });

                history.ListAnswer = history.ListAnswer.Where(a => a.Answer != null).ToList();

                return history;
            }).ToList();

            return listHistory;
            
        }

        string ArrangeString (string numbers)
        {
            var listNumber = numbers.Split('_');

            var listNumberTrans = new List<int>();
            foreach (var item in listNumber)
            {
                listNumberTrans.Add(Convert.ToInt16(item));
            }
            listNumberTrans.Sort();

            return String.Join('_', listNumberTrans);
        }
    }
}
