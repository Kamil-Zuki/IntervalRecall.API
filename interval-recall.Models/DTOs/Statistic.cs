namespace interval_recall.Models.DTOs
{
    public class Statistic
    {
        public Guid QuestionGroupId { get; set; }
        public string Title { get; set; }
        public List<StatisticQuestionDTO>? Questions { get; set; }
    }
}
