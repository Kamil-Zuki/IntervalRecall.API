namespace interval_recall.Models.DTOs
{
    public class StatisticQuestionDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public TimeSpan Interval { get; set; }
        public DateTime RepetitionDate { get; set; }
        public string State { get; set; }
        public TimeSpan Step { get; set; }
        public double EasyFactor { get; set; }
        public int Repetitions { get; set; }
    }
}
