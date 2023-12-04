
namespace interval_recall.Models.DTOs
{
    public class OutRecallQuestionGroupDTO
    {
        public Guid QuestionGroupId { get; set; }
        public string Title { get; set; }
        public List<OutQuestionDTO> Questions { get; set; }
    }
}
