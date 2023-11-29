namespace interval_recall.BLL.DTOs
{
    public class QuestionDTO
    {
        public List<bool> Qualities { get; set; }
        public TimeSpan Interval { get; set; }
        public double IntervalModifier { get; set; }
        public double NewInterval { get; set; }
        public DateTime RepetitionDate { get; set; }
        public string State { get; set; }
        public TimeSpan Step { get; set; }
        public double EasyFactor { get; set; }
        public double EasyBonus { get; set; }
        public int Repetitions { get; set; }
    }
}
