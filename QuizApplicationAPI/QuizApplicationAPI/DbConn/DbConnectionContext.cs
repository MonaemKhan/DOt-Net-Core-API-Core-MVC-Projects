using Microsoft.EntityFrameworkCore;
using QuizApplicationAPI.Model;

namespace QuizApplicationAPI.DbConn
{
    public class DbConnectionContext:DbContext
    {
        public DbConnectionContext(DbContextOptions<DbConnectionContext> options):base(options)
        {
            
        }

        public DbSet<QuizQuestion> QuestionTable { get; set; }
    }
}
