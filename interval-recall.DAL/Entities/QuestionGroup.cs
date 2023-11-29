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
        public double IntervalModifier { get; set; }
        public double EasyBonus { get; set; }
        public double NewInterval {get; set; }

        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Question>? Questions { get; set; }
    }
}
