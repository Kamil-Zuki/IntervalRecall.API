namespace interval_recall.BLL.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Interval { get; set; }
        public double EasyFactor { get; set; }
        public int Repetitions { get; set; }
        public DateTime RepetitionDate { get; set; }



        public ICollection<DecisionQuality> DecisionQualitys { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
