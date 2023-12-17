using interval_recall.BLL.Interfaces;
using interval_recall.DAL.EF;
using interval_recall.DAL.Entities;
using interval_recall.Models.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace interval_recall.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IntervalRecallContext _dataContext;
        public QuestionService(IntervalRecallContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task CreateRangeAsync(List<InQuestionDTO> questionDTOs)
        {
            _dataContext.Questions.AddRange(questionDTOs.Select(questionDTO => new Question()
            {
                Text = questionDTO.Text,
                QuestionGroupId = questionDTO.QuestionGroupId,
                Answers = questionDTO.Answers.Select(answerDTO => new Answer()
                {
                    Value = answerDTO.Value,
                    IsCorrect = answerDTO.IsCorrect
                }).ToList()
            }).ToList()
            );
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<OutRecallQuestionGroupDTO>> GetRecallQuestionsAsync(Guid? questionGroupId)
        {
            try
            {
                if (questionGroupId != null)
                {
                    var questionGroup = await _dataContext.QuestionGroups
                    .Where(qGroup => (questionGroupId == null ? true : qGroup.Id == questionGroupId) /*&& qGroup.Questions.Any(x => DateTime.Now >= x.RepetitionDate)*/)
                    .Include(qGroup => qGroup.Questions) // .Where(question => DateTime.Now >=/* question.RepetitionDate*/ DateTime.MinValue)
                    .ThenInclude(q => q.Answers)
                    .FirstOrDefaultAsync();

                    var random = new Random();
                    var newQuestions = questionGroup.Questions
                        .Where(q => q.State == "New")
                        .OrderBy(q => random.Next())
                        .Take(questionGroup.AmountOfNew)
                        .ToList();

                    var learnAndGraduatedQuestions = questionGroup.Questions
                        .Where(q => q.State == "Learning" || q.State == "Graduated")
                        .OrderBy(q => random.Next())
                        .Take(questionGroup.AmountOfLearn)
                        .ToList();

                    List<Question> questions = new();
                    questions.AddRange(newQuestions);
                    questions.AddRange(learnAndGraduatedQuestions);

                    return new List<OutRecallQuestionGroupDTO>
                    {
                        new OutRecallQuestionGroupDTO()
                        {
                            QuestionGroupId = questionGroup.Id,
                            Title = questionGroup.Title,
                            Questions = questions.Select(q => new OutQuestionDTO()
                            {
                                QuestionId = q.Id,
                                Text = q.Text,
                                State = q.State,
                                Answers = q.Answers.Select(a => new OutAnswerDTO()
                                {
                                    AnswerId = a.Id,
                                    Value = a.Value
                                }).ToList()
                            }).ToList()
                        }
                    };
                }

                else
                {
                    var questionGroups = _dataContext.QuestionGroups
                    .Where(qGroup => (questionGroupId == null ? true : qGroup.Id == questionGroupId) /*&& qGroup.Questions.Any(x => DateTime.Now >= x.RepetitionDate)*/)
                    .Include(qGroup => qGroup.Questions/*.Where(question => DateTime.Now >= question.RepetitionDate)*/)
                    .ThenInclude(q => q.Answers)
                    .ToList();

                    return questionGroups.Select(g => new OutRecallQuestionGroupDTO()
                    {
                        QuestionGroupId = g.Id,
                        Title = g.Title,
                        Questions = g.Questions.Select(q => new OutQuestionDTO()
                        {
                            QuestionId = q.Id,
                            Text = q.Text,
                            State = q.State,
                            Answers = q.Answers.Select(a => new OutAnswerDTO()
                            {
                                AnswerId = a.Id,
                                Value = a.Value
                            }).ToList()
                        }).ToList(),
                    }).ToList();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }




        public async Task GetAnswersToQuestionsAsync(List<InUserResponceDTO> userResponces)
        {

        }

        public async Task GetGroupQuestionsAsync()
        {

        }
        public async Task UpdateAsync()
        {

        }
        public async Task DeleteQuestion()
        {

        }

    }
}
