
using ASM_PH35423.Data.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ASM_PH35423.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser ,IdentityRole <Guid> , Guid>(options)
    {
        public DbSet<Exam> Exams { get;set; }
        public DbSet<ExamHistory> ExamHistories { get;set; }
        public DbSet<Question> Questions { get;set; }
        public DbSet<ExamHistoryDetails> ExamHistoryDetails { get;set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = C#5_ASM;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=true");       // thiết lập làm việc với SqlServer

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
