namespace interval_recall.BLL.DTOs
{
    public class QuestionDTO
    {
        public bool[] Qualities { get; set; }
        public int Interval { get; set; }
        public double IntervalModifier { get; set; }
        public double NewInterval { get; set; }
        public DateTime RepetitionDate { get; set; }
        public double EasyFactor { get; set; }
        public double EasyBonus { get; set; }
        public int Repetitions { get; set; }
    }
}
