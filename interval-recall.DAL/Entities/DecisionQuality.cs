using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace interval_recall.DAL.Entities
{
    public class DecisionQuality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public bool Value { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
