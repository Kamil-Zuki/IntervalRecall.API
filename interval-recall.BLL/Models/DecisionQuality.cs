namespace interval_recall.BLL.Models
{
    public class DecisionQuality
    {
        public Guid Id { get; set; }
        public bool Value { get; set; }

        public Guid QuestionId { get; set; }
    }
}
