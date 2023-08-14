using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.DbCon;
using MiniInventoryManagementSystem.Models;
using MiniInventoryManagementSystem.Models.SupportClass;

namespace MiniInventoryManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DbConnectionContext _context;

        public CustomerController(DbConnectionContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _context.CustomerTable.ToListAsync());
        }

        //create action start from here
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer cust)
        {
            if(!ModelState.IsValid)
            {
                return View(cust);
            }
            var data = await _context.CustomerTable.ToListAsync();
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].CustomerName == cust.CustomerName && data[i].CustomerEmail==cust.CustomerEmail 
                    && data[i].CustomerPhone == cust.CustomerPhone)
                {
                    var ErrorMessage = new SupportClassErrorView()
                    {
                        IsError = true,
                        ErrorType = "Duplicate Value",
                        ErrorMessage = cust.CustomerName + " is already exits"
                    };
                    return RedirectToAction("Create", ErrorMessage);
                }
            }
            await _context.CustomerTable.AddAsync(cust);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create(SupportClassErrorView error)
        {
            TempData["errormessage"] = error;
            return View();
        }
        //create action end here


        //Edit action start from here
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _context.CustomerTable.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //Edit action ends here

        //Details action start from here
        public async Task<ActionResult> Details(int id)
        {
            return View(await _context.CustomerTable.FindAsync(id));
        }
        //Details action ends here

        //Delete Action start from here
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _context.CustomerTable.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Customer customer)
        {
            _context.CustomerTable.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //Delete action ends here
    }
}
