
namespace interval_recall.Models.DTOs
{
    public class OutRecallQuestionGroupDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<OutQuestionDTO> Questions { get; set; }
    }
}
