namespace interval_recall.Models.DTOs
{
    public class OutQuestionDTO
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }

        public virtual ICollection<OutAnswerDTO> Answers { get; set; }
    }
}
