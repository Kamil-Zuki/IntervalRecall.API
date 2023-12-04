using interval_recall.DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace interval_recall.DAL.EF
{
    public class IntervaRecallContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<QuestionGroup> QuestionGroups { get; set; }
        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<DecisionQuality> DecisionQualities { get; set; }

        private readonly string _databasePath;

        public IntervaRecallContext(DbContextOptions<IntervaRecallContext> options)
            : base(options)
        {
            //_databasePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "interval-recall.db");
            _databasePath = @"C:\Users\karat\Desktop\Projects\interval-recall\interval-recall.API\wwwroot" + @"\interval-recall.db";
            //Database.Migrate();
            //Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source="+ _databasePath);

    }
}
