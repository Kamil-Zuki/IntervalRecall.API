using interval_recall.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Int64 IntervalTicks { get; set; } = 60;
        [NotMapped]
        public TimeSpan Interval
        {
            get { return TimeSpan.FromTicks(IntervalTicks); }
            set { IntervalTicks = value.Ticks; }
        }
        public double EasyFactor { get; set; } = 2.5;
        public int Repetitions { get; set; } = 0;
        public DateTime RepetitionDate { get; set; } = DateTime.Now;
        public string State { get; set; } = Enum.GetName(typeof(States), States.New)!;
        public TimeSpan Step { get; set; } = TimeSpan.FromMinutes(1);
        public Guid QuestionGroupId { get; set; }
        public virtual QuestionGroup QuestionGroup { get; set; }

        public virtual ICollection<DecisionQuality> DecisionQualities { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
