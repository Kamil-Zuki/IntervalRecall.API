namespace interval_recall.Models.DTOs
{
    public class InQuestionDTO
    {
        public string Text { get; set; }
        public Guid QuestionGroupId { get; set; }

        public ICollection<InAnswerDTO> Answers { get; set; }
    }
}
