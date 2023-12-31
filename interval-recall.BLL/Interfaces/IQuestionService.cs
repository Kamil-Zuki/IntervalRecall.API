﻿using interval_recall.Models.DTOs;

namespace interval_recall.BLL.Interfaces
{
    public interface IQuestionService
    {
        Task CreateRangeAsync(List<InQuestionDTO> questionDTOs);
        Task<List<OutRecallQuestionGroupDTO>> GetRecallQuestionsAsync(Guid? questionGroupId);
        Task<QuestionsAmountInfo> GetQuestionsAmountInfoAsync(Guid questionGroupId);
    }
}
