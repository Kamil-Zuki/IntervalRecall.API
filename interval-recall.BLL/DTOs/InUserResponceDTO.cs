namespace interval_recall.BLL.DTOs
{
    public class InUserResponceDTO
    {
        public Guid QuestionId { get; set; }
        public ICollection<Guid> AnswerIds { get; set; }
    }
}
