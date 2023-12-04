namespace interval_recall.Models.DTOs
{
    public class InUserResponceDTO
    {
        public Guid QuestionId { get; set; }
        public ICollection<Guid> AnswerIds { get; set; }
    }
}
