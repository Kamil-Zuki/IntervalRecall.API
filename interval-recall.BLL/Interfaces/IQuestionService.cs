using interval_recall.BLL.DTOs;
using interval_recall.DAL.Entities;

namespace interval_recall.BLL.Interfaces
{
    public interface IQuestionService
    {
        Task CreateRangeAsync(List<InQuestionDTO> questionDTOs);
        Task<List<OutQuestionDTO>> GetRecallQuestions();
    }
}
