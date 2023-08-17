using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.DbCon;
using MiniInventoryManagementSystem.Models;
using MiniInventoryManagementSystem.Models.SupportClass;

namespace MiniInventoryManagementSystem.Controllers
{
    public class StockProfitController : Controller
    {
        private readonly DbConnectionContext _context;
        public StockProfitController(DbConnectionContext context)
        {
            _context = context;
        }
                
        public async Task<ActionResult> Index()
        {
            var catagory = await _context.CatagoryTable.ToListAsync();
            var product = await _context.ProductTable.ToListAsync();
            var purches_details = await _context.PurchesDetailsTable.ToListAsync();
            var sales_details = await _context.SalesDetailsTable.ToListAsync();

            var item = from pro in product
                       join ca in catagory on pro.Product_CatagoryId equals ca.CatagoryId
                       join pd in purches_details on pro.ProductId equals pd.PurchesDetails_ProductId into purchesGroup
                       join sd in sales_details on pro.ProductId equals sd.SalesDetails_ProductId into salesGroup
                       select new
                       {
                           CatagoryName = ca.CatagoryName,
                           ProductName = pro.ProductName,
                           PurchesQuentity = purchesGroup.Sum(p=>p.PurchesDetailsQuantity),
                           PurchesPrice = purchesGroup.Sum(p=>p.PurchesDetailsPrice),
                           TotalPurchesPrice = purchesGroup.Sum(p=>p.PurchesDetailsPrice*p.PurchesDetailsQuantity),
                           SalesQuentity = salesGroup.Sum(p=>p.SalesDetailsQuantity),
                           SalesPrice = salesGroup.Sum(p=>p.SalesDetailsPrice),
                           TotalSalePrice = salesGroup.Sum(p=>p.SalesDetailsPrice*p.SalesDetailsQuantity),
                           Stock = (purchesGroup.Sum(p => p.PurchesDetailsQuantity)) -(salesGroup.Sum(p => p.SalesDetailsQuantity)),
                           profit = (salesGroup.Sum(p => p.SalesDetailsPrice * p.SalesDetailsQuantity))-
                                     (purchesGroup.Sum(p => p.PurchesDetailsPrice * p.PurchesDetailsQuantity))
                       };
            List<StockAndProfitOverview> sp = new List<StockAndProfitOverview>();
            foreach(var d in item)
            {
                sp.Add(new StockAndProfitOverview()
                {
                    CatagoryName = d.CatagoryName,
                    ProductName = d.ProductName,
                    PurchesDetailsQuantity = (int)d.PurchesQuentity,
                    PurchesDetailsPrice = d.PurchesPrice,
                    ParchesDetailsTotalPrice = d.TotalPurchesPrice,
                    SalesDetailsQuantity = d.SalesQuentity,
                    SalesDetailsPrice = d.SalesPrice,
                    SalesDetailsTotalPrice = d.TotalSalePrice,
                    Profit = d.profit,
                    stock = (int)d.Stock
                });
            }
            return View(sp);
        }

        //profit view
        public async Task<ActionResult> Profit()
        {
            var catagory = await _context.CatagoryTable.ToListAsync();
            var product = await _context.ProductTable.ToListAsync();
            var purches_details = await _context.PurchesDetailsTable.ToListAsync();
            var sales_details = await _context.SalesDetailsTable.ToListAsync();
            return View(prof(catagory, product, purches_details, sales_details));
        }
        //profit function
        public List<Profit> prof(List<Catagory> catagory, List<Product> product, List<PurchesDetails> purches_details, List<SalesDetails> sales_details)
        {
            var ex = purches_details.GroupBy(p => p.PurchesDetails_ProductId).Select(gr => new
            {
                ProductId = gr.Key,
                AveragePurches = gr.Average(p => p.PurchesDetailsPrice)
            });

            var item = from pro in product
                       join ca in catagory on pro.Product_CatagoryId equals ca.CatagoryId
                       join e in ex on pro.ProductId equals e.ProductId
                       join sd in sales_details on pro.ProductId equals sd.SalesDetails_ProductId
                       select new
                       {
                           pro.ProductId,
                           pro.ProductName,
                           ca.CatagoryName,
                           sd.SalesDetailsPrice,
                           sd.SalesDetailsQuantity,
                           e.AveragePurches
                       };

            var item2 = item.GroupBy(p => p.ProductId).Select(gr => new
            {
                CatagoryName = gr.Select(p => p.CatagoryName).ToArray()[0],
                ProductName = gr.Select(p => p.ProductName).ToArray()[0],
                ParchesDetailsTotalPrice = gr.Sum(p => p.AveragePurches * p.SalesDetailsQuantity),
                TotalSaleDetailsQuentity = gr.Sum(p => p.SalesDetailsQuantity),
                SalesDetailsTotalPrice = gr.Sum(p => p.SalesDetailsQuantity * p.SalesDetailsPrice),
            });

            List<Profit> profit = new List<Profit>();
            foreach (var d in item2)
            {
                profit.Add(new Profit()
                {
                    CatagoryName = d.CatagoryName,
                    ProductName = d.ProductName,
                    ParchesDetailsTotalPrice = d.ParchesDetailsTotalPrice,
                    TotalSaleDetailsQuentity = d.TotalSaleDetailsQuentity,
                    SalesDetailsTotalPrice = d.SalesDetailsTotalPrice,
                    Net_Profit = d.SalesDetailsTotalPrice - d.ParchesDetailsTotalPrice
                });
            }
            return profit;
        }
        //profit ends

        //stock view
        [HttpGet]
        public async Task<ActionResult> Stock()
        {
            var catagory = await _context.CatagoryTable.ToListAsync();
            var product = await _context.ProductTable.ToListAsync();
            var purches_details = await _context.PurchesDetailsTable.ToListAsync();
            var sales_details = await _context.SalesDetailsTable.ToListAsync();
                      
            return View(stock(catagory,product,purches_details,sales_details));
        }
        //stock function
        public List<Stock> stock(List<Catagory> catagory,List<Product> product,List<PurchesDetails> purches_details,List<SalesDetails> sales_details)
        {
            var item = from pro in product
                       join ca in catagory on pro.Product_CatagoryId equals ca.CatagoryId
                       join pd in purches_details on pro.ProductId equals pd.PurchesDetails_ProductId into purchesGroup
                       join sd in sales_details on pro.ProductId equals sd.SalesDetails_ProductId into salesGroup
                       select new
                       {
                           CatagoryName = ca.CatagoryName,
                           ProductName = pro.ProductName,
                           PurchesQuentity = purchesGroup.Sum(p => p.PurchesDetailsQuantity),
                           SalesQuentity = salesGroup.Sum(p => p.SalesDetailsQuantity),
                           Stock = (purchesGroup.Sum(p => p.PurchesDetailsQuantity)) - (salesGroup.Sum(p => p.SalesDetailsQuantity)),
                       };

            List<Stock> stk = new List<Stock>();
            foreach (var d in item)
            {
                stk.Add(new Stock()
                {
                    CatagoryName = d.CatagoryName,
                    ProductName = d.ProductName,
                    PurchesDetailsQuantity = (int)d.PurchesQuentity,
                    SalesDetailsQuantity = d.SalesQuentity,
                    stock = (int)d.Stock
                });
            }
            return stk;
        }
        //stock ends


        //daily profit
        public async Task<ActionResult> DailyProfit()
        {
            var catagory = await _context.CatagoryTable.ToListAsync();
            var product = await _context.ProductTable.ToListAsync();
            var purches_details = await _context.PurchesDetailsTable.ToListAsync();
            var sales_details = await _context.SalesDetailsTable.ToListAsync();
            var sale = await _context.SalesTable.ToListAsync();
            var ex = purches_details.GroupBy(p => p.PurchesDetails_ProductId).Select(gr => new
            {
                ProductId = gr.Key,
                AveragePurches = gr.Average(p => p.PurchesDetailsPrice)
            });

            string date = DateTime.Now.ToString("yyyy-MM-dd");

            var item = from pro in product
                       join ca in catagory on pro.Product_CatagoryId equals ca.CatagoryId
                       join e in ex on pro.ProductId equals e.ProductId
                       join sd in sales_details on pro.ProductId equals sd.SalesDetails_ProductId
                       join sa in sale on sd.SalesDetails_SalesId equals sa.SalesId
                       where (sa.SalesDate == date)
                       select new
                       {
                           pro.ProductId,
                           pro.ProductName,
                           ca.CatagoryName,
                           sd.SalesDetailsPrice,
                           sd.SalesDetailsQuantity,
                           e.AveragePurches
                       };

            var item2 = item.GroupBy(p => p.ProductId).Select(gr => new
            {
                CatagoryName = gr.Select(p => p.CatagoryName).ToArray()[0],
                ProductName = gr.Select(p => p.ProductName).ToArray()[0],
                ParchesDetailsTotalPrice = gr.Sum(p => p.AveragePurches * p.SalesDetailsQuantity),
                TotalSaleDetailsQuentity = gr.Sum(p => p.SalesDetailsQuantity),
                SalesDetailsTotalPrice = gr.Sum(p => p.SalesDetailsQuantity * p.SalesDetailsPrice),
            });

            List<Profit> profit = new List<Profit>();
            foreach (var d in item2)
            {
                profit.Add(new Profit()
                {
                    CatagoryName = d.CatagoryName,
                    ProductName = d.ProductName,
                    ParchesDetailsTotalPrice = d.ParchesDetailsTotalPrice,
                    TotalSaleDetailsQuentity = d.TotalSaleDetailsQuentity,
                    SalesDetailsTotalPrice = d.SalesDetailsTotalPrice,
                    Net_Profit = d.SalesDetailsTotalPrice - d.ParchesDetailsTotalPrice
                });
            }
            return View(profit);
        }

        //MonthlyProfit
        public async Task<ActionResult> MonthlyProfit()
        {
            var catagory = await _context.CatagoryTable.ToListAsync();
            var product = await _context.ProductTable.ToListAsync();
            var purches_details = await _context.PurchesDetailsTable.ToListAsync();
            var sales_details = await _context.SalesDetailsTable.ToListAsync();
            var sale = await _context.SalesTable.ToListAsync();
            var ex = purches_details.GroupBy(p => p.PurchesDetails_ProductId).Select(gr => new
            {
                ProductId = gr.Key,
                AveragePurches = gr.Average(p => p.PurchesDetailsPrice)
            });

            string date = DateTime.Now.ToString("yyyy-MM-dd");

            var item = from pro in product
                       join ca in catagory on pro.Product_CatagoryId equals ca.CatagoryId
                       join e in ex on pro.ProductId equals e.ProductId
                       join sd in sales_details on pro.ProductId equals sd.SalesDetails_ProductId
                       join sa in sale on sd.SalesDetails_SalesId equals sa.SalesId
                       where (sa.SalesDate.Substring(0, 4) == date.Substring(0, 4) && sa.SalesDate.Substring(5, 2) == date.Substring(5, 2))
                       select new
                       {
                           pro.ProductId,
                           pro.ProductName,
                           ca.CatagoryName,
                           sd.SalesDetailsPrice,
                           sd.SalesDetailsQuantity,
                           e.AveragePurches
                       };

            var item2 = item.GroupBy(p => p.ProductId).Select(gr => new
            {
                CatagoryName = gr.Select(p => p.CatagoryName).ToArray()[0],
                ProductName = gr.Select(p => p.ProductName).ToArray()[0],
                ParchesDetailsTotalPrice = gr.Sum(p => p.AveragePurches * p.SalesDetailsQuantity),
                TotalSaleDetailsQuentity = gr.Sum(p => p.SalesDetailsQuantity),
                SalesDetailsTotalPrice = gr.Sum(p => p.SalesDetailsQuantity * p.SalesDetailsPrice),
            });

            List<Profit> profit = new List<Profit>();
            foreach (var d in item2)
            {
                profit.Add(new Profit()
                {
                    CatagoryName = d.CatagoryName,
                    ProductName = d.ProductName,
                    ParchesDetailsTotalPrice = d.ParchesDetailsTotalPrice,
                    TotalSaleDetailsQuentity = d.TotalSaleDetailsQuentity,
                    SalesDetailsTotalPrice = d.SalesDetailsTotalPrice,
                    Net_Profit = d.SalesDetailsTotalPrice - d.ParchesDetailsTotalPrice
                });
            }
            return View(profit);
        }
    }
}
