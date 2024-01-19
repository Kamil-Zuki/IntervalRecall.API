using interval_recall.Models.DTOs;

namespace interval_recall.BLL.Interfaces
{
    public interface ILearningService
    {
        Task<List<QuestionResponseInfo>> RecallAsync(List<InUserResponceDTO> userResponces);
    }
}
