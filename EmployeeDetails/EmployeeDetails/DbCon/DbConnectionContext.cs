using EmployeeDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.DbCon
{
    public class DbConnectionContext:DbContext
    {
        public DbConnectionContext(DbContextOptions<DbConnectionContext> options):base(options)
        {
            
        }

        public DbSet<City> CityTable { get; set; }
        public DbSet<Country> CountryTable { get;set; }
        public DbSet<State> StateTable { get; set; }
        public DbSet<Employee> EmployeeTable { get; set; }

    }
}
