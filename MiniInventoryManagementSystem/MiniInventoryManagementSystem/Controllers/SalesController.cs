using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.DbCon;
using MiniInventoryManagementSystem.Models;
using MiniInventoryManagementSystem.Models.SupportClass;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace MiniInventoryManagementSystem.Controllers
{
    public class SalesController : Controller
    {
        private readonly DbConnectionContext _context;
        public SalesController(DbConnectionContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            List<Sales> sale = await _context.SalesTable.ToListAsync();
            List<Customer> customer = await _context.CustomerTable.ToListAsync();
            List<SalesMan> salesman = await _context.SalesManTable.ToListAsync();
            List<Product> product = await _context.ProductTable.ToListAsync();
            List<SalesDetails> salesdetails = await _context.SalesDetailsTable.ToListAsync();
            List<Catagory> catagories = await _context.CatagoryTable.ToListAsync();

            var item = from sa in sale
                       join cust in customer on sa.Sales_CustomerId equals cust.CustomerId
                       join sm in salesman on sa.Sales_SalesManId equals sm.SalesManId
                       join sd in salesdetails on sa.SalesId equals sd.SalesDetails_SalesId
                       join p in product on sd.SalesDetails_ProductId equals p.ProductId
                       join ca in catagories on p.Product_CatagoryId equals ca.CatagoryId
                       select new { sa.SalesId,sa.Sales_SalesManId,sa.Sales_CustomerId,sa.SalesDate,
                            sm.SalesManName,cust.CustomerName,
                            sd.SalesDetailsId,sd.SalesDetailsPrice,sd.SalesDetailsQuantity,sd.SalesDetails_ProductId,
                            p.ProductName,ca.CatagoryName};
            List<Sales_and_SalesDetails> salesandsalesdetails = new List<Sales_and_SalesDetails>();
            foreach(var da in item)
            {
                salesandsalesdetails.Add(
                    new Sales_and_SalesDetails()
                    {
                        SalesDetailsId = da.SalesDetailsId,
                        SalesDetailsPrice = da.SalesDetailsPrice,
                        SalesDetailsQuantity = da.SalesDetailsQuantity,
                        SalesDetails_ProductId = da.SalesDetails_ProductId,
                        SalesDetails_ProductName = da.ProductName,
                        catagoryName = da.CatagoryName,

                         //sales
                        SalesId = da.SalesId,
                        Sales_SalesManId = da.Sales_SalesManId,
                        Sales_SalesManName = da.SalesManName,
                        Sales_CustomerId = da.Sales_CustomerId,
                        Sales_CustomerName = da.CustomerName,
                        SalesDate = da.SalesDate,
                        TotalPrice = da.SalesDetailsPrice*da.SalesDetailsQuantity
                    });
            }
            return View(salesandsalesdetails);
        }
       //Index Action ends here

        //To Sale Product All the work Done is start Here
        public async Task<ActionResult> Create()
        {
            ViewBag.CustomerList = await _context.CustomerTable.ToListAsync();
            ViewBag.SalesManList = await _context.SalesManTable.ToListAsync();
            ViewBag.ProductList = await _context.ProductTable.ToListAsync();
            ViewBag.IsError = false;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Sales_and_SalesDetails ssd)
        {
            //for stock
            var product = await _context.ProductTable.ToListAsync();
            var purches_details = await _context.PurchesDetailsTable.ToListAsync();
            var sales_details = await _context.SalesDetailsTable.ToListAsync();

            var item = from pro in product
                       join pd in purches_details on pro.ProductId equals pd.PurchesDetails_ProductId into purchesGroup
                       join sd in sales_details on pro.ProductId equals sd.SalesDetails_ProductId into salesGroup
                       where(pro.ProductId == ssd.SalesDetails_ProductId)
                       select new
                       {
                           Stock = (purchesGroup.Sum(p => p.PurchesDetailsQuantity)) - (salesGroup.Sum(p => p.SalesDetailsQuantity)),
                       };
            foreach(var i in item)
            {
                if(i.Stock < ssd.SalesDetailsQuantity)
                {
                    ViewBag.CustomerList = await _context.CustomerTable.ToListAsync();
                    ViewBag.SalesManList = await _context.SalesManTable.ToListAsync();
                    ViewBag.ProductList = await _context.ProductTable.ToListAsync();
                    ViewBag.IsError = true;
                    ViewBag.errorType = "Stock Out";
                    ViewBag.errorMassage = "Product Is Out Of Stock";
                    return View(ssd);
                }
            }


            List<Sales> sale = new List<Sales>()
            {
                new Sales()
                {
                    Sales_SalesManId = ssd.Sales_SalesManId,
                    Sales_CustomerId = ssd.Sales_CustomerId,
                    SalesDate = ssd.SalesDate,
                }
            };
            await _context.SalesTable.AddAsync(sale[0]);
            await _context.SaveChangesAsync();
            var data = await _context.SalesTable.ToListAsync();
            List<SalesDetails> salesDetails = new List<SalesDetails>()
            {
                new SalesDetails()
                {
                    SalesDetailsPrice = ssd.SalesDetailsPrice,
                    SalesDetailsQuantity = ssd.SalesDetailsQuantity,
                    SalesDetails_SalesId = data[data.Count-1].SalesId,
                    SalesDetails_ProductId = ssd.SalesDetails_ProductId
                }
            };
            await _context.SalesDetailsTable.AddAsync(salesDetails[0]);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //To Seal Product All the work Done is ends Here

        //Edit action start from here
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var salesdetails = await _context.SalesDetailsTable.FindAsync(id);

            var sale = await _context.SalesTable.FindAsync(salesdetails.SalesDetails_SalesId);
            var customer = await _context.CustomerTable.FindAsync(sale.Sales_CustomerId);
            var salesman = await _context.SalesManTable.FindAsync(sale.Sales_SalesManId);

            var product = await _context.ProductTable.FindAsync(salesdetails.SalesDetails_ProductId);
            var catagories = await _context.CatagoryTable.FindAsync(product.Product_CatagoryId);

            List<Sales_and_SalesDetails> ssd = new List<Sales_and_SalesDetails>()
            {
                new Sales_and_SalesDetails()
                {
                        SalesDetailsId = salesdetails.SalesDetailsId,
                        SalesDetailsPrice = salesdetails.SalesDetailsPrice,
                        SalesDetailsQuantity = salesdetails.SalesDetailsQuantity,
                        SalesDetails_ProductId = salesdetails.SalesDetails_ProductId,
                        SalesDetails_ProductName = product.ProductName,
                        catagoryName = catagories.CatagoryName,

                         //sales
                        SalesId = sale.SalesId,
                        Sales_SalesManId = sale.Sales_SalesManId,
                        Sales_SalesManName = salesman.SalesManName,
                        Sales_CustomerId = sale.Sales_CustomerId,
                        Sales_CustomerName = customer.CustomerName,
                        SalesDate = sale.SalesDate,
                        TotalPrice = salesdetails.SalesDetailsPrice*salesdetails.SalesDetailsQuantity
                }
            };

            ViewBag.ProductList = await _context.ProductTable.ToListAsync();
            ViewBag.SalesManList = await _context.SalesManTable.ToListAsync();
            ViewBag.CustomerList = await _context.CustomerTable.ToListAsync();
            return View(ssd[0]);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Sales_and_SalesDetails salesdetails)
        {
            List<Sales> sale = new List<Sales>()
            {
                new Sales()
                {
                    SalesId = salesdetails.SalesId,
                    Sales_SalesManId = salesdetails.Sales_SalesManId,
                    Sales_CustomerId = salesdetails.Sales_CustomerId,
                    SalesDate = salesdetails.SalesDate
                }
            };

            List<SalesDetails> saled = new List<SalesDetails>()
            {
                new SalesDetails()
                {
                    SalesDetailsId = salesdetails.SalesDetailsId,
                    SalesDetailsPrice = salesdetails.SalesDetailsPrice,
                    SalesDetailsQuantity = salesdetails.SalesDetailsQuantity,
                    SalesDetails_SalesId = salesdetails.SalesId,
                    SalesDetails_ProductId = salesdetails.SalesDetails_ProductId
                }
            };

            _context.Entry(sale[0]).State = EntityState.Modified;
            _context.Entry(saled[0]).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //Edit Action ends here

        //Details Action Start from here
        //[HttpGet]
        public async Task<ActionResult> Details(int Id)
        {
            var salesdetails = await _context.SalesDetailsTable.FindAsync(Id);

            var sale = await _context.SalesTable.FindAsync(salesdetails.SalesDetails_SalesId);
            var customer = await _context.CustomerTable.FindAsync(sale.Sales_CustomerId);
            var salesman = await _context.SalesManTable.FindAsync(sale.Sales_SalesManId);

            var product = await _context.ProductTable.FindAsync(salesdetails.SalesDetails_ProductId);
            var catagories = await _context.CatagoryTable.FindAsync(product.Product_CatagoryId);

            List<Sales_and_SalesDetails> ssd = new List<Sales_and_SalesDetails>()
            {
                new Sales_and_SalesDetails()
                {
                        SalesDetailsId = salesdetails.SalesDetailsId,
                        SalesDetailsPrice = salesdetails.SalesDetailsPrice,
                        SalesDetailsQuantity = salesdetails.SalesDetailsQuantity,
                        SalesDetails_ProductId = salesdetails.SalesDetails_ProductId,
                        SalesDetails_ProductName = product.ProductName,
                        catagoryName = catagories.CatagoryName,

                         //sales
                        SalesId = sale.SalesId,
                        Sales_SalesManId = sale.Sales_SalesManId,
                        Sales_SalesManName = salesman.SalesManName,
                        Sales_CustomerId = sale.Sales_CustomerId,
                        Sales_CustomerName = customer.CustomerName,
                        SalesDate = sale.SalesDate,
                        TotalPrice = salesdetails.SalesDetailsPrice*salesdetails.SalesDetailsQuantity
                }
            };
            return View(ssd[0]);
        }
        //Details action ends here

        //Delete Action Start from here
        public async Task<ActionResult> Delete(int Id)
        {
            var salesdetails = await _context.SalesDetailsTable.FindAsync(Id);

            var sale = await _context.SalesTable.FindAsync(salesdetails.SalesDetails_SalesId);
            var customer = await _context.CustomerTable.FindAsync(sale.Sales_CustomerId);
            var salesman = await _context.SalesManTable.FindAsync(sale.Sales_SalesManId);

            var product = await _context.ProductTable.FindAsync(salesdetails.SalesDetails_ProductId);
            var catagories = await _context.CatagoryTable.FindAsync(product.Product_CatagoryId);

            List<Sales_and_SalesDetails> ssd = new List<Sales_and_SalesDetails>()
            {
                new Sales_and_SalesDetails()
                {
                        SalesDetailsId = salesdetails.SalesDetailsId,
                        SalesDetailsPrice = salesdetails.SalesDetailsPrice,
                        SalesDetailsQuantity = salesdetails.SalesDetailsQuantity,
                        SalesDetails_ProductId = salesdetails.SalesDetails_ProductId,
                        SalesDetails_ProductName = product.ProductName,
                        catagoryName = catagories.CatagoryName,

                         //sales
                        SalesId = sale.SalesId,
                        Sales_SalesManId = sale.Sales_SalesManId,
                        Sales_SalesManName = salesman.SalesManName,
                        Sales_CustomerId = sale.Sales_CustomerId,
                        Sales_CustomerName = customer.CustomerName,
                        SalesDate = sale.SalesDate,
                        TotalPrice = salesdetails.SalesDetailsPrice*salesdetails.SalesDetailsQuantity
                }
            };
            return View(ssd[0]);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Sales_and_SalesDetails salesdetails)
        {
            var s = await _context.SalesTable.FindAsync(salesdetails.SalesId);
            var sd = await _context.SalesDetailsTable.FindAsync(salesdetails.SalesDetailsId);

            _context.SalesTable.Remove(s);
            _context.SalesDetailsTable.Remove(sd);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //current date sale
        public async Task<ActionResult> CurrentDateSale()
        {
            List<Sales> sale = await _context.SalesTable.ToListAsync();
            List<Customer> customer = await _context.CustomerTable.ToListAsync();
            List<SalesMan> salesman = await _context.SalesManTable.ToListAsync();
            List<Product> product = await _context.ProductTable.ToListAsync();
            List<SalesDetails> salesdetails = await _context.SalesDetailsTable.ToListAsync();
            List<Catagory> catagories = await _context.CatagoryTable.ToListAsync();

            string date = DateTime.Now.ToString("yyyy-MM-dd");

            var item = from sa in sale
                       join cust in customer on sa.Sales_CustomerId equals cust.CustomerId
                       join sm in salesman on sa.Sales_SalesManId equals sm.SalesManId
                       join sd in salesdetails on sa.SalesId equals sd.SalesDetails_SalesId
                       join p in product on sd.SalesDetails_ProductId equals p.ProductId
                       join ca in catagories on p.Product_CatagoryId equals ca.CatagoryId
                       where (sa.SalesDate == date)
                       select new
                       {
                           sa.SalesId,
                           sa.Sales_SalesManId,
                           sa.Sales_CustomerId,
                           sa.SalesDate,
                           sm.SalesManName,
                           cust.CustomerName,
                           sd.SalesDetailsId,
                           sd.SalesDetailsPrice,
                           sd.SalesDetailsQuantity,
                           sd.SalesDetails_ProductId,
                           p.ProductName,
                           ca.CatagoryName
                       };
            List<Sales_and_SalesDetails> salesandsalesdetails = new List<Sales_and_SalesDetails>();
            foreach (var da in item)
            {
                salesandsalesdetails.Add(
                    new Sales_and_SalesDetails()
                    {
                        SalesDetailsId = da.SalesDetailsId,
                        SalesDetailsPrice = da.SalesDetailsPrice,
                        SalesDetailsQuantity = da.SalesDetailsQuantity,
                        SalesDetails_ProductId = da.SalesDetails_ProductId,
                        SalesDetails_ProductName = da.ProductName,
                        catagoryName = da.CatagoryName,

                        //sales
                        SalesId = da.SalesId,
                        Sales_SalesManId = da.Sales_SalesManId,
                        Sales_SalesManName = da.SalesManName,
                        Sales_CustomerId = da.Sales_CustomerId,
                        Sales_CustomerName = da.CustomerName,
                        SalesDate = da.SalesDate,
                        TotalPrice = da.SalesDetailsPrice * da.SalesDetailsQuantity
                    });
            }
            return View(salesandsalesdetails);
        }

        //current month
        public async Task<ActionResult> CurrentMonthSale()
        {
            List<Sales> sale = await _context.SalesTable.ToListAsync();
            List<Customer> customer = await _context.CustomerTable.ToListAsync();
            List<SalesMan> salesman = await _context.SalesManTable.ToListAsync();
            List<Product> product = await _context.ProductTable.ToListAsync();
            List<SalesDetails> salesdetails = await _context.SalesDetailsTable.ToListAsync();
            List<Catagory> catagories = await _context.CatagoryTable.ToListAsync();

            string date = DateTime.Now.ToString("yyyy-MM-dd");

            var item = from sa in sale
                       join cust in customer on sa.Sales_CustomerId equals cust.CustomerId
                       join sm in salesman on sa.Sales_SalesManId equals sm.SalesManId
                       join sd in salesdetails on sa.SalesId equals sd.SalesDetails_SalesId
                       join p in product on sd.SalesDetails_ProductId equals p.ProductId
                       join ca in catagories on p.Product_CatagoryId equals ca.CatagoryId
                       where (sa.SalesDate.Substring(0, 4) == date.Substring(0, 4) && sa.SalesDate.Substring(5, 2) == date.Substring(5, 2))
                       select new
                       {
                           sa.SalesId,
                           sa.Sales_SalesManId,
                           sa.Sales_CustomerId,
                           sa.SalesDate,
                           sm.SalesManName,
                           cust.CustomerName,
                           sd.SalesDetailsId,
                           sd.SalesDetailsPrice,
                           sd.SalesDetailsQuantity,
                           sd.SalesDetails_ProductId,
                           p.ProductName,
                           ca.CatagoryName
                       };
            List<Sales_and_SalesDetails> salesandsalesdetails = new List<Sales_and_SalesDetails>();
            foreach (var da in item)
            {
                salesandsalesdetails.Add(
                    new Sales_and_SalesDetails()
                    {
                        SalesDetailsId = da.SalesDetailsId,
                        SalesDetailsPrice = da.SalesDetailsPrice,
                        SalesDetailsQuantity = da.SalesDetailsQuantity,
                        SalesDetails_ProductId = da.SalesDetails_ProductId,
                        SalesDetails_ProductName = da.ProductName,
                        catagoryName = da.CatagoryName,

                        //sales
                        SalesId = da.SalesId,
                        Sales_SalesManId = da.Sales_SalesManId,
                        Sales_SalesManName = da.SalesManName,
                        Sales_CustomerId = da.Sales_CustomerId,
                        Sales_CustomerName = da.CustomerName,
                        SalesDate = da.SalesDate,
                        TotalPrice = da.SalesDetailsPrice * da.SalesDetailsQuantity
                    });
            }
            return View(salesandsalesdetails);
        }
    }
}
