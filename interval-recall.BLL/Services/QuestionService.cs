using interval_recall.BLL.DTOs;
using interval_recall.BLL.Interfaces;
using interval_recall.DAL.EF;
using interval_recall.DAL.Entities;
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

        public async Task<List<OutQuestionDTO>> GetRecallQuestions()
        {
            return await _dataContext.Questions.Where(x => DateTime.Now.Date >= x.RepetitionDate.Date )
                .Select(questionDTO => new OutQuestionDTO()
                {
                    Id = questionDTO.Id,
                    Text = questionDTO.Text,
                    Answers = questionDTO.Answers.Select(answerDTO => new OutAnswerDTO()
                    {
                        Id = answerDTO.Id,
                        Value = answerDTO.Value
                    }).ToList()
                }).ToListAsync();
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
