namespace interval_recall.Models.DTOs
{
    public class QuestionResponceInfo
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Repetitions { get; set; }
        public DateTime RepetitionDate { get; set; }

        public ICollection<DecisionQualityDTO> DecisionQualities { get; set; }
    }
}
