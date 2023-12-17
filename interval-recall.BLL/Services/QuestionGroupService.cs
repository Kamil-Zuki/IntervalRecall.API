using interval_recall.BLL.Interfaces;
using interval_recall.DAL.EF;
using interval_recall.DAL.Entities;
using interval_recall.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace interval_recall.BLL.Services
{
    public class QuestionGroupService : IQuestionGroupService
    {
        private readonly IntervalRecallContext _dataContext;
        public QuestionGroupService(IntervalRecallContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateAsync(InQuestionGroupDTO questionGroupDTO)
        {
            _dataContext.QuestionGroups.Add(new QuestionGroup()
            {
                Title = questionGroupDTO.Title,
                AmountOfNew = questionGroupDTO.AmountOfNew,
                AmountOfLearn = questionGroupDTO.AmountOfLearn,
                IntervalModifier = questionGroupDTO.IntervalModifier,
                EasyBonus = questionGroupDTO.EasyBonus,
                NewInterval = questionGroupDTO.NewInterval
            });
            await _dataContext.SaveChangesAsync();

        }
        public async Task<List<OutQuestionGroupDTO>> GetAllAsync()
        {
            return await _dataContext.QuestionGroups.Select(x => new OutQuestionGroupDTO()
            {
                Id = x.Id,
                Title = x.Title,
                AmountOfNew = x.AmountOfNew,
                AmountOfLearn = x.AmountOfLearn,
                IntervalModifier = x.IntervalModifier,
                NewInterval = x.NewInterval,
                EasyBonus = x.EasyBonus,
                UserId = x.UserId,

            }).ToListAsync();
        }
        public async Task UpdateAsync()
        {

        }
        public async Task DeleteAsync()
        {

        }
        public async Task ShowStatisticsAsync()
        {

        }
        public async Task EraseStatisticsAsync()
        {

        }
    }
}
