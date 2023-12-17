using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace interval_recall.DAL.Entities
{
    public class User
    {
        public User()
        {
            QuestionGroups = new HashSet<QuestionGroup>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public Guid? UserGroupId { get; set; }

        public virtual UserGroup? UserGroup { get; set; }

        public virtual ICollection<QuestionGroup> QuestionGroups { get; set; }

    }
}
