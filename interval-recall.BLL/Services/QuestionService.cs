using interval_recall.BLL.Interfaces;
using interval_recall.DAL.EF;
using interval_recall.DAL.Entities;
using interval_recall.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace interval_recall.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IntervaRecallContext _dataContext;
        public QuestionService(IntervaRecallContext dataContext)
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
            var questionGroups = _dataContext.QuestionGroups.Where(qGroup => (questionGroupId == null ? true : qGroup.Id == questionGroupId) && qGroup.Questions.Any(x => DateTime.Now.Date >= x.RepetitionDate.Date))
                .Include(qGroup => qGroup.Questions)
                .ThenInclude(q => q.Answers);


            return questionGroups.Select(g => new OutRecallQuestionGroupDTO()
            {
                QuestionGroupId = g.Id,
                Title = g.Title,
                Questions = g.Questions.Select(q => new OutQuestionDTO()
                {
                    QuestionId = q.Id,
                    Text = q.Text,
                    Answers = q.Answers.Select(a => new OutAnswerDTO()
                    {
                        AnswerId = a.Id,
                        Value = a.Value
                    }).ToList()
                }).ToList(),
            }).ToList();
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
