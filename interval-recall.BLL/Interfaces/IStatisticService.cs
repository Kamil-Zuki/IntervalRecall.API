using interval_recall.Models.DTOs;

namespace interval_recall.BLL.Interfaces
{
    public interface IStatisticService
    {
        Task RemoveStatisticAsync(Guid questionGroupId);
        Task<List<QuestionGroupStatistic>> GetStatisticAsync(Guid? questionGroupId);
    }
}
