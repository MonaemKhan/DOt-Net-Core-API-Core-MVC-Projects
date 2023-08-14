using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.Models;
using MiniInventoryManagementSystem.Models.SupportClass;

namespace MiniInventoryManagementSystem.DbCon
{
    public class DbConnectionContext : DbContext
    {
        public DbConnectionContext(DbContextOptions<DbConnectionContext> options) : base(options)
        {

        }

        public DbSet<Catagory> CatagoryTable {set;get;}
        public DbSet<Product> ProductTable { set; get; }
        public DbSet<Customer> CustomerTable { set; get; }
        public DbSet<Supplyer> SupplyerTable { set; get; }
        public DbSet<SalesMan> SalesManTable { set; get; }
        public DbSet<Sales> SalesTable { set; get; }
        public DbSet<SalesDetails> SalesDetailsTable { set; get; }

        public DbSet<Purches> PurchesTable { set; get; }
        public DbSet<PurchesDetails> PurchesDetailsTable { set; get; }
    }
}
