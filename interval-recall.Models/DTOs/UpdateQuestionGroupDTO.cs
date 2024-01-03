namespace interval_recall.Models.DTOs
{
    public class UpdateQuestionGroupDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int AmountOfNew { get; set; }
        public int AmountOfLearn {  get; set; }

    }
}
