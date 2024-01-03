using interval_recall.BLL.Interfaces;
using interval_recall.DAL.EF;
using interval_recall.DAL.Entities;
using interval_recall.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace interval_recall.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IntervalRecallContext _dataContext;
        private readonly IMapper _mapper;
        public QuestionService(IntervalRecallContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
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
            }).ToList());
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

                    var newQuestions = questionGroup.Questions
                        .Where(q => q.State == "New")
                        .OrderByDescending(q => q.RepetitionDate)
                        .Take(questionGroup.AmountOfNew)
                        .ToList();

                    var learnAndGraduatedQuestions = questionGroup.Questions
                        .Where(q => q.State != "New")
                        .OrderByDescending(q => q.RepetitionDate)
                        .Take(questionGroup.AmountOfLearn)
                        .ToList();

                    List<Question> questions = [.. newQuestions, .. learnAndGraduatedQuestions];
                    //questions.OrderByDescending(q => q.State == "New" ? 1 : 0);

                    return new List<OutRecallQuestionGroupDTO>
                    {
                        new OutRecallQuestionGroupDTO()
                        {
                            Id = questionGroup.Id,
                            Title = questionGroup.Title,
                            Questions = questions.Select(q => new OutQuestionDTO()
                            {
                                Id = q.Id,
                                Text = q.Text,
                                State = q.State,
                                Answers = q.Answers.Select(a => new OutAnswerDTO()
                                {
                                    Id = a.Id,
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
                        Id = g.Id,
                        Title = g.Title,
                        Questions = g.Questions.Select(q => new OutQuestionDTO()
                        {
                            Id = q.Id,
                            Text = q.Text,
                            State = q.State,
                            Answers = q.Answers.Select(a => new OutAnswerDTO()
                            {
                                Id = a.Id,
                                Value = a.Value
                            }).ToList()
                        }).ToList(),
                    }).ToList();
                }
            }
            catch (Exception ex)
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
