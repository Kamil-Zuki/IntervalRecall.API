using interval_recall.Models.DTOs;

namespace interval_recall.BLL.Interfaces
{
    public interface ILearningService
    {
        Task<(int, int)> RecallAsync(List<InUserResponceDTO> userResponces);
    }
}
