﻿using interval_recall.Models.DTOs;

namespace interval_recall.BLL.Interfaces
{
    public interface IQuestionGroupService
    {
        Task CreateAsync(InQuestionGroupDTO questionGroupDTO);
        Task<List<OutQuestionGroupDTO>> GetAllAsync();
        Task<OutQuestionGroupDTO?> GetByIdAsync(Guid questionGroupId);
        Task UpdateAsync(OutQuestionGroupDTO questionGroupDTO);
    }
}
