using interval_recall.BLL.DTOs;

namespace interval_recall.BLL.Interfaces
{
    public interface ILearningService
    {
        Task Recall(List<InUserResponceDTO> userResponces); 
    }
}
