namespace interval_recall.Models.DTOs
{
    public class OutQuestionDTO
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string Text { get; set; }
        public DateTime RepetitionDate { get; set; }
        public virtual ICollection<OutAnswerDTO> Answers { get; set; }
    }
}
