using interval_recall.BLL.Interfaces;
using interval_recall.DAL.EF;
using interval_recall.DAL.Entities;
using interval_recall.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace interval_recall.BLL.Services
{
    public class QuestionGroupService : IQuestionGroupService
    {
        private readonly IntervalRecallContext _dataContext;
        private readonly IMapper _mapper;
        public QuestionGroupService(IntervalRecallContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
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
        public async Task UpdateAsync(UpdateQuestionGroupDTO questionGroupDTO)
        {
            var questionGroup = _dataContext.QuestionGroups.Where(q => q.Id == questionGroupDTO.Id);
            if (questionGroup.Count() == 0)
                throw new Exception("Question group not found");

            _dataContext.QuestionGroups.Update(_mapper.Map<QuestionGroup>(questionGroupDTO));
            await _dataContext.SaveChangesAsync();
        }
        public async Task DeleteAsync()
        {

        }

    }
}
