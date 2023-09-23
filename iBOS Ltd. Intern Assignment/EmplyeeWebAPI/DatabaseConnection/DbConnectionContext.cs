using EmplyeeWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EmplyeeWebAPI.DatabaseConnection
{
    public class DbConnectionContext : DbContext
    {
        public DbConnectionContext(DbContextOptions<DbConnectionContext> options):base(options)
        {
            
        }

        public DbSet<EmployeeDetails> EmployeeDetailsTable { get; set; }
        public DbSet<EmployeePresent> EmployeeAttendenceTable { get; set; }
    }
}
