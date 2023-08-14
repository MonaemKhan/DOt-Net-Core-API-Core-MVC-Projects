using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.DbCon;
using MiniInventoryManagementSystem.Models;
using MiniInventoryManagementSystem.Models.SupportClass;

namespace MiniInventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbConnectionContext _context;

        public ProductController(DbConnectionContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _context.ProductTable.Include(x=>x.Product_Catagory).ToListAsync());
        }

        //create action start from here
        public async Task<ActionResult> Create()
        {
            ViewBag.CatagoryList = await _context.CatagoryTable.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            //product.ProductId = Convert.ToInt32(product.ProductId);
            //product.Product_Catagory = await _context.CatagoryTable.FindAsync()
            //if (!ModelState.IsValid)
            //{
            //    ViewBag.CatagoryList = await _context.CatagoryTable.ToListAsync();
            //    return View(product);
            //}
            if(product.Product_CatagoryId == null)
            {
                var ErrorMessage = new SupportClassErrorView()
                {
                    IsError = true,
                    ErrorType = "Catagory Name Not Selected",
                    ErrorMessage = "Please Select A Catagory"
                };
                return RedirectToAction("Create", ErrorMessage);
            }
            product.Product_Catagory = await _context.CatagoryTable.FindAsync(product.Product_CatagoryId);
            var data = await _context.ProductTable.Include(x=>x.Product_Catagory).ToListAsync();
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].ProductName == product.ProductName && data[i].Product_Catagory.CatagoryName == product.Product_Catagory.CatagoryName)
                {
                    var ErrorMessage = new SupportClassErrorView()
                    {
                        IsError = true,
                        ErrorType = "Duplicate Value",
                        ErrorMessage = "Product is already exits"
                    };
                    return RedirectToAction("Create", ErrorMessage);
                }
            }
            await _context.ProductTable.AddAsync(product);
            await _context.SaveChangesAsync();
            ViewBag.CatagoryList = await _context.CatagoryTable.ToListAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Create(SupportClassErrorView error)
        {
            ViewBag.CatagoryList = await _context.CatagoryTable.ToListAsync();
            TempData["errormessage"] = error;
            return View();
        }
        //create action ends here

        //edit action start from here
        public async Task<ActionResult> Edit(int id)
        {
            var data = await _context.ProductTable.FindAsync(id);
            ViewBag.CatagoryList = await _context.CatagoryTable.ToListAsync();
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            ViewBag.CatagoryList = await _context.CatagoryTable.ToListAsync();
            return RedirectToAction("Index");
        }
        //edit action ends here

        //Details action start here
        public async Task<ActionResult> Details(int id)
        {
            var data = await _context.ProductTable.FindAsync(id);
            ViewBag.CatagoryList = await _context.CatagoryTable.ToListAsync();
            return View(data);
        }
        //Details Action Ends Here

        //Delete Action Start from Here
        public async Task<ActionResult> Delete(int Id)
        {
            var data = await _context.ProductTable.FindAsync(Id);
            ViewBag.CatagoryList = await _context.CatagoryTable.ToListAsync();
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Product product)
        {
            _context.ProductTable.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
