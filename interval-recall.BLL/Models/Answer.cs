namespace interval_recall.BLL.Models
{
    public class Answer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Value { get; set; }
        public bool IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
    }
}
