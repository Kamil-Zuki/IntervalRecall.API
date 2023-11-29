using interval_recall.DAL.Entities;

namespace interval_recall.BLL.DTOs
{
    public class OutQuestionDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public virtual ICollection<OutAnswerDTO> Answers { get; set; }
    }
}
