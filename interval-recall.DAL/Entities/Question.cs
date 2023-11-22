using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace interval_recall.DAL.Entities
{
    public class Question
    {
        public Question()
        {
            DecisionQualities = new HashSet<DecisionQuality>();
            Answers = new HashSet<Answer>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Interval { get; set; }
        public double EasyFactor { get; set; }
        public int Repetitions { get; set; }
        public DateTime RepetitionDate { get; set; }



        public virtual ICollection<DecisionQuality> DecisionQualities { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
