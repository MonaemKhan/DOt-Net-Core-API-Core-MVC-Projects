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
        public async Task<ActionResult> CurrentMonth()
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

            string date = DateTime.Now.ToString("yyyy-MM-dd");

            //current momth calculation start
            var CurrentMonth_01 = from sm in salesman
                                  join s in sales on sm.SalesManId equals s.Sales_SalesManId
                                  join sd in salesdetails on s.SalesId equals sd.SalesDetails_SalesId
                                  join pro in product on sd.SalesDetails_ProductId equals pro.ProductId
                                  join e in ex on pro.ProductId equals e.ProductId
                                  where (s.SalesDate.Substring(0, 4) == date.Substring(0, 4) && s.SalesDate.Substring(5, 2) == date.Substring(5, 2))
                                  select new
                                  {
                                      sm.SalesManId,
                                      sm.SalesManName,
                                      sm.SalesManDesignation,
                                      sd.SalesDetailsPrice,
                                      sd.SalesDetailsQuantity,
                                      e.AveragePurches
                                  };

            var CurrentMonth_02 = CurrentMonth_01.GroupBy(p => p.SalesManId).Select(gr => new
            {
                SalesManId = gr.Key,
                SalesManName = gr.Select(p => p.SalesManName).ToArray()[0],
                SalesManDesignation = gr.Select(p => p.SalesManDesignation).ToArray()[0],
                CurrentMonthSalePrice = gr.Sum(p => p.SalesDetailsPrice * p.SalesDetailsQuantity),
                CurrentMonthSaleQuentity = gr.Sum(p => p.SalesDetailsQuantity),
                CurrrentMonthPurchesPrice = gr.Sum(p => p.SalesDetailsQuantity * p.AveragePurches)
            });

            List<SalesManCommission> smc = new List<SalesManCommission>();
            foreach (var data in CurrentMonth_02)
            {
                smc.Add(new SalesManCommission()
                {
                    SalesManName = data.SalesManName,
                    SalesManDesignation = data.SalesManDesignation,
                    CurrentMonthTotalSales = data.CurrentMonthSaleQuentity,
                    CureentMonthCommission = (data.CurrentMonthSalePrice-data.CurrrentMonthPurchesPrice)*0.10
                });
            }
            return View(smc);
        }
        public async Task<ActionResult> CurrentDate()
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

            string date = DateTime.Now.ToString("yyyy-MM-dd");

            //current date calculation
            var CurrentDate_01 = from sm in salesman
                                  join s in sales on sm.SalesManId equals s.Sales_SalesManId
                                  join sd in salesdetails on s.SalesId equals sd.SalesDetails_SalesId
                                  join pro in product on sd.SalesDetails_ProductId equals pro.ProductId
                                  join e in ex on pro.ProductId equals e.ProductId
                                  where(s.SalesDate == date)
                                  select new
                                  {
                                      sm.SalesManId,
                                      sm.SalesManName,
                                      sm.SalesManDesignation,
                                      sd.SalesDetailsPrice,
                                      sd.SalesDetailsQuantity,
                                      e.AveragePurches
                                  };

            var CurrentDate_02 = CurrentDate_01.GroupBy(p => p.SalesManId).Select(gr => new
            {
                SalesManId = gr.Key,
                SalesManName = gr.Select(p => p.SalesManName).ToArray()[0],
                SalesManDesignation = gr.Select(p => p.SalesManDesignation).ToArray()[0],
                CurrentDateSalePrice = gr.Sum(p => p.SalesDetailsPrice * p.SalesDetailsQuantity),
                CurrentDateSaleQuentity = gr.Sum(p => p.SalesDetailsQuantity),
                CurrentDatePurchesPrice = gr.Sum(p => p.SalesDetailsQuantity * p.AveragePurches)
            });
                       
            List < SalesManCommission > smc = new List<SalesManCommission>();
            foreach(var data in CurrentDate_02)
            {
                smc.Add(new SalesManCommission()
                {
                    SalesManName = data.SalesManName,
                    SalesManDesignation = data.SalesManDesignation,
                    CurrentdateTotalSales= data.CurrentDateSaleQuentity,
                    CurrentDateCommission = (data.CurrentDateSalePrice-data.CurrentDatePurchesPrice)*0.10
                });
            }
            return View(smc);
        }
    }
}
