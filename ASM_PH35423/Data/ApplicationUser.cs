using ASM_PH35423.Data.Tables;
using Microsoft.AspNetCore.Identity;

namespace ASM_PH35423.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<Exam> Exams { get; set; }

        public ICollection<ExamHistory> ExamHistories { get; set; }
    }

}
