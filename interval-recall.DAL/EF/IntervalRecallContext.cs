using interval_recall.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace interval_recall.DAL.EF
{
    public class IntervalRecallContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<QuestionGroup> QuestionGroups { get; set; }
        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<DecisionQuality> DecisionQualities { get; set; }

        private readonly string _databasePath;

        public IntervalRecallContext(DbContextOptions<IntervalRecallContext> options, IConfiguration configuration)
            : base(options)
        {
            //_databasePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "interval-recall.db");
            _databasePath = configuration.GetConnectionString("DefaultConnection");
            //Database.Migrate();
            //Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite(@"Data Source=" + _databasePath);
            }

        }

    }
}
