namespace interval_recall.Models.DTOs
{
    public class QuestionGroupStatistic
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<StatisticQuestionDTO> Questions { get; set; }

    }
}
