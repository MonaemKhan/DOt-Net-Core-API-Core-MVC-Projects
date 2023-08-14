using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.DbCon;
using MiniInventoryManagementSystem.Models.SupportClass;

namespace MiniInventoryManagementSystem.Controllers
{
    public class SaleManCommissionController : Controller
    {
        private readonly DbConnectionContext _context;
        public SaleManCommissionController(DbConnectionContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var salesman = await _context.SalesManTable.ToListAsync();
            var sales = await _context.SalesTable.ToListAsync();
            var salesdetails = await _context.SalesDetailsTable.ToListAsync();
            var purchesdetails = await _context.PurchesDetailsTable.ToListAsync();
            var product = await _context.ProductTable.ToListAsync();

            var ex = purchesdetails.GroupBy(p => p.PurchesDetails_ProductId).Select(gr => new
            {
                ProductId = gr.Key,
                AveragePurches = gr.Average(p => p.PurchesDetailsPrice)
            });
            
            var item1 = from sm in salesman
                      join s in sales on sm.SalesManId equals s.Sales_SalesManId
                      join sd in salesdetails on s.SalesId equals sd.SalesDetails_SalesId
                      join pro in product on sd.SalesDetails_ProductId equals pro.ProductId
                      join e in ex on pro.ProductId equals e.ProductId
                      select new
                      {
                          sm.SalesManId,
                          sm.SalesManName,
                          sm.SalesManDesignation,
                          sd.SalesDetailsPrice,
                          sd.SalesDetailsQuantity,
                          pro.ProductId,
                          pro.ProductName,
                          e.AveragePurches
                      };

            var item2 = item1.GroupBy(p => p.ProductId & p.SalesManId).Select(gr => new
            {
                ProductName = gr.Select(p=>p.ProductName),
                SalesManName = gr.Select(p=>p.SalesManName),
                SalesManDesignation = gr.Select(p => p.SalesManDesignation),
                TotalSalesDetailsPrice = gr.Sum(p => p.SalesDetailsPrice * p.SalesDetailsQuantity),
                TotalSalesDetailsQuantity = gr.Sum(p => p.SalesDetailsQuantity),
                TotalPurchesDetailsPrice = gr.Sum(p => p.SalesDetailsQuantity * p.AveragePurches)
            });
            List<SalesManCommission> smc = new List<SalesManCommission>();
            foreach(var data in item2)
            {
                smc.Add(new SalesManCommission()
                {
                    ProductName = data.ProductName.ToArray()[0],
                    SalesManName = data.SalesManName.ToArray()[0],
                    SalesManDesignation = data.SalesManDesignation.ToArray()[0],
                    TotalSalesDetailsPrice = data.TotalSalesDetailsPrice,
                    TotalSalesDetailsQuantity = data.TotalSalesDetailsQuantity,
                    TotalPurchesDetailsPrice = data.TotalPurchesDetailsPrice,
                    Commission = (data.TotalSalesDetailsPrice- data.TotalPurchesDetailsPrice) *0.10
                });
            }
            return View(smc);
        }
    }
}
