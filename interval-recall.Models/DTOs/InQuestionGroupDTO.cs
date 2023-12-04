namespace interval_recall.Models.DTOs
{
    public class InQuestionGroupDTO
    {
        public string Title { get; set; }
        //public int New { get; set; }
        //public int Learn { get; set; }
        public double IntervalModifier { get; set; } = 1.0;
        public double EasyBonus { get; set; } = 1.3;
        public double NewInterval { get; set; } = 0.2;
        public Guid? UserId { get; set; }
    }
}
