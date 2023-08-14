using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.DbCon;
using MiniInventoryManagementSystem.Migrations;
using MiniInventoryManagementSystem.Models;
using MiniInventoryManagementSystem.Models.SupportClass;

namespace MiniInventoryManagementSystem.Controllers
{
    public class PurchesController : Controller
    {
        private readonly DbConnectionContext _context;
        public PurchesController(DbConnectionContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            List<Purches> purches = await _context.PurchesTable.ToListAsync();
            List<PurchesDetails> purchesDetails = await _context.PurchesDetailsTable.ToListAsync();
            List<Product> products = await _context.ProductTable.ToListAsync();
            List<Supplyer> supplyers = await _context.SupplyerTable.ToListAsync();
            List<Catagory> catagories = await _context.CatagoryTable.ToListAsync();

            var item = from p in purches
                       join s in supplyers on p.Purches_SupplyerId equals s.SupplyerId
                       join pd in purchesDetails on p.PurchesId equals pd.PurchesDetails_PurchesId
                       join pro in products on pd.PurchesDetails_ProductId equals pro.ProductId
                       join ca in catagories on pro.Product_CatagoryId equals ca.CatagoryId
                       select new
                       {
                           p.PurchesId,p.PurchesDate,
                           s.SupplyerId,s.SupplyerName,
                           pd.PurchesDetailsId,pd.PurchesDetailsPrice,pd.PurchesDetailsQuantity,
                           pro.ProductId,pro.ProductName,
                           ca.CatagoryName
                       };
            List<Purches_And_PurchesDetails> ppd = new List<Purches_And_PurchesDetails>();
            foreach(var da in item)
            {
                ppd.Add( new Purches_And_PurchesDetails()
                {
                    PurchesDetailsId = da.PurchesDetailsId,
                    PurchesDetails_ProductId = da.ProductId,
                    ProductName = da.ProductName,
                    catagoryName = da.CatagoryName,
                    PurchesDetailsPrice = da.PurchesDetailsPrice,
                    PurchesDetailsQuantity = da.PurchesDetailsQuantity,
                    PurchesId = da.PurchesId,
                    PurchesDate = da.PurchesDate,
                    Purches_SupplyerId = da.SupplyerId,
                    SupplyerName = da.SupplyerName,
                    TotalPrice = da.PurchesDetailsPrice*da.PurchesDetailsQuantity
                });
            }
            return View(ppd);
        }

        //create action start from here
        public async Task<ActionResult> Create()
        {
            ViewBag.SupplyerList = await _context.SupplyerTable.ToListAsync();
            ViewBag.ProductList = await _context.ProductTable.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Purches_And_PurchesDetails PPD)
        {
            List<Purches> purches = new List<Purches>()
            {
                new Purches()
                {
                    PurchesDate = PPD.PurchesDate,
                    Purches_SupplyerId = PPD.Purches_SupplyerId
                }
            };
            await _context.PurchesTable.AddAsync(purches[0]);
            await _context.SaveChangesAsync();

            var data = await _context.PurchesTable.ToListAsync();


            List<PurchesDetails> purchesDetails = new List<PurchesDetails>()
            {
                new PurchesDetails()
                {
                    PurchesDetails_PurchesId = data[data.Count-1].PurchesId,
                    PurchesDetails_ProductId = PPD.PurchesDetails_ProductId,
                    PurchesDetailsPrice = PPD.PurchesDetailsPrice,
                    PurchesDetailsQuantity = PPD.PurchesDetailsQuantity
                }
            };
            await _context.PurchesDetailsTable.AddAsync(purchesDetails[0]);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //create action ends here

        //edit action start from here
        public async Task<ActionResult> Edit(int Id)
        {
            var pd = await _context.PurchesDetailsTable.FindAsync(Id);
            var p = await _context.PurchesTable.FindAsync(pd.PurchesDetails_PurchesId);
            var pro = await _context.ProductTable.FindAsync(pd.PurchesDetails_ProductId);
            var sup = await _context.SupplyerTable.FindAsync(p.Purches_SupplyerId);
            List<Purches_And_PurchesDetails> ppd = new List<Purches_And_PurchesDetails>()
            {
                new Purches_And_PurchesDetails()
                {
                    PurchesDetailsId = pd.PurchesDetailsId,
                    PurchesDetails_ProductId = pd.PurchesDetails_ProductId,
                    ProductName = pro.ProductName,
                    PurchesDetailsPrice = pd.PurchesDetailsPrice,
                    PurchesDetailsQuantity = pd.PurchesDetailsQuantity,
                    PurchesId = p.PurchesId,
                    PurchesDate =  p.PurchesDate,
                    Purches_SupplyerId =p.Purches_SupplyerId,
                    SupplyerName = sup.SupplyerName
                }
            };
            ViewBag.SupplyerList = await _context.SupplyerTable.ToListAsync();
            ViewBag.ProductList = await _context.ProductTable.ToListAsync();
            return View(ppd[0]);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Purches_And_PurchesDetails PPD)
        {
            List<Purches> purches = new List<Purches>()
            {
                new Purches()
                {
                    PurchesId = PPD.PurchesId,
                    PurchesDate = PPD.PurchesDate,
                    Purches_SupplyerId = PPD.Purches_SupplyerId
                }
            };
            List<PurchesDetails> purchesDetails = new List<PurchesDetails>()
            {
                new PurchesDetails()
                {
                    PurchesDetailsId = PPD.PurchesDetailsId,
                    PurchesDetails_PurchesId = PPD.PurchesId,
                    PurchesDetails_ProductId = PPD.PurchesDetails_ProductId,
                    PurchesDetailsPrice = PPD.PurchesDetailsPrice,
                    PurchesDetailsQuantity = PPD.PurchesDetailsQuantity
                }
            };
            _context.Entry(purches[0]).State = EntityState.Modified;
            _context.Entry(purchesDetails[0]).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //Edit action ends here

        //Details action start here
        public async Task<ActionResult> Details(int Id)
        {
            var pd = await _context.PurchesDetailsTable.FindAsync(Id);
            var p = await _context.PurchesTable.FindAsync(pd.PurchesDetails_PurchesId);
            var pro = await _context.ProductTable.FindAsync(pd.PurchesDetails_ProductId);
            var sup = await _context.SupplyerTable.FindAsync(p.Purches_SupplyerId);
            var ca = await _context.CatagoryTable.FindAsync(pro.Product_CatagoryId);
            List<Purches_And_PurchesDetails> ppd = new List<Purches_And_PurchesDetails>()
            {
                new Purches_And_PurchesDetails()
                {
                    PurchesDetailsId = pd.PurchesDetailsId,
                    PurchesDetails_ProductId = pd.PurchesDetails_ProductId,
                    ProductName = pro.ProductName,
                    catagoryName = ca.CatagoryName,
                    PurchesDetailsPrice = pd.PurchesDetailsPrice,
                    PurchesDetailsQuantity = pd.PurchesDetailsQuantity,
                    PurchesId = p.PurchesId,
                    PurchesDate =  p.PurchesDate,
                    Purches_SupplyerId =p.Purches_SupplyerId,
                    SupplyerName = sup.SupplyerName,
                    TotalPrice = pd.PurchesDetailsPrice*pd.PurchesDetailsQuantity
                }
            };
            return View(ppd[0]);
        }
        //details action ends here

        //Delete action start from here
        public async Task<ActionResult> Delete(int Id)
        {
            var pd = await _context.PurchesDetailsTable.FindAsync(Id);
            var p = await _context.PurchesTable.FindAsync(pd.PurchesDetails_PurchesId);
            var pro = await _context.ProductTable.FindAsync(pd.PurchesDetails_ProductId);
            var sup = await _context.SupplyerTable.FindAsync(p.Purches_SupplyerId);
            var ca = await _context.CatagoryTable.FindAsync(pro.Product_CatagoryId);
            List<Purches_And_PurchesDetails> ppd = new List<Purches_And_PurchesDetails>()
            {
                new Purches_And_PurchesDetails()
                {
                    PurchesDetailsId = pd.PurchesDetailsId,
                    PurchesDetails_ProductId = pd.PurchesDetails_ProductId,
                    ProductName = pro.ProductName,
                    catagoryName = ca.CatagoryName,
                    PurchesDetailsPrice = pd.PurchesDetailsPrice,
                    PurchesDetailsQuantity = pd.PurchesDetailsQuantity,
                    PurchesId = p.PurchesId,
                    PurchesDate =  p.PurchesDate,
                    Purches_SupplyerId =p.Purches_SupplyerId,
                    SupplyerName = sup.SupplyerName,
                    TotalPrice = pd.PurchesDetailsPrice*pd.PurchesDetailsQuantity
                }
            };
            return View(ppd[0]);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Purches_And_PurchesDetails PPD)
        {
            var pd = await _context.PurchesDetailsTable.FindAsync(PPD.PurchesDetailsId);
            var p = await _context.PurchesTable.FindAsync(pd.PurchesDetails_PurchesId);

            _context.PurchesDetailsTable.Remove(pd);
            _context.PurchesTable.Remove(p);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
