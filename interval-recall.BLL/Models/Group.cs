namespace interval_recall.BLL.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double IntervalModifier { get; set; }
        public double EasyBonus { get; set; }
        public double NewInterval {get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
