namespace interval_recall.Models.DTOs
{
    public class StatisticQuestionGroup
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<StatisticQuestionDTO> Questions { get; set; }

    }
}
