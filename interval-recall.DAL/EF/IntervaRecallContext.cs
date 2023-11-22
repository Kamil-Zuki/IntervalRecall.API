using interval_recall.DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace interval_recall.DAL.EF
{
    public class IntervaRecallContext : DbContext
    {
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Question> Qestions { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<DecisionQuality> DecisionQualities { get; set; }

        private readonly string _databasePath;

        public IntervaRecallContext()
        {
            _databasePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "interval-recall.db");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source="+ _databasePath);

    }
}
