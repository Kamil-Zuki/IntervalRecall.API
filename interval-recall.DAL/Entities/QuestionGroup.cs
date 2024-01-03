using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace interval_recall.DAL.Entities
{
    public class QuestionGroup
    {
        public QuestionGroup()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int AmountOfNew { get; set; }
        public int AmountOfLearn { get; set; }
        public double IntervalModifier { get; set; } = 1.0;
        public double EasyBonus { get; set; } = 1.3;
        public double NewInterval {get; set; } = 0.2;

        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Question>? Questions { get; set; }
    }
}
