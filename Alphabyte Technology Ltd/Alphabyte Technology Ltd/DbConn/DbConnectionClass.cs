using Alphabyte_Technology_Ltd_.Models;
using Microsoft.EntityFrameworkCore;

namespace Alphabyte_Technology_Ltd_.DbConn
{
    public class DbConnectionClass: DbContext
    {
        public DbConnectionClass(DbContextOptions<DbConnectionClass> options):base(options)
        {
            
        }
        public DbSet<InterViewerDetails> InterViewerDetailsTable { get; set; }
        public DbSet<Division> DivisionTable { get; set; }
        public DbSet<Department> DepartmentTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InterViewerDetails>()
                .HasData(new
                {
                    Id = "10000001",
                    Name = "Ashraful Alam",
                    InterViewerDetails_DivisionId = 1,
                    InterViewerDetails_DepartmentDept_Id = 5,
                    DoB = "2000-09-07",
                    ResumeFile = "File1.pdf"
                }, new
                {
                    Id = "10000002",
                    Name = "M.A. Monaem Khan",
                    InterViewerDetails_DivisionId = 1,
                    InterViewerDetails_DepartmentDept_Id = 1,
                    DoB = "1998-04-21",
                    ResumeFile = "File2.pdf"
                });

            modelBuilder.Entity<Division>()
                .HasData(new
                {
                    DivisionId = 1,
                    DivisionName = "IT"
                }, new
                {
                    DivisionId = 2,
                    DivisionName = "BBA"
                });

            modelBuilder.Entity<Department>()
                .HasData(new
                {
                    Dept_Id = 1,
                    DeptName = "CSE",
                    Department_DivisionId = 1
                }, new
                {
                    Dept_Id = 2,
                    DeptName = "SE",
                    Department_DivisionId = 1
                }, new
                {
                    Dept_Id = 3,
                    DeptName = "MGT",
                    Department_DivisionId = 2
                }, new
                {
                    Dept_Id = 4,
                    DeptName = "THM",
                    Department_DivisionId = 2
                }, new
                {
                    Dept_Id = 5,
                    DeptName = "Software",
                    Department_DivisionId = 1
                });

        }
    }
}
