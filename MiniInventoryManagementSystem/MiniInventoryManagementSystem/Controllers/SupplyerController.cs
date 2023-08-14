using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.DbCon;
using MiniInventoryManagementSystem.Models;
using MiniInventoryManagementSystem.Models.SupportClass;

namespace MiniInventoryManagementSystem.Controllers
{
    public class SupplyerController : Controller
    {
        private readonly DbConnectionContext _context;
        public SupplyerController(DbConnectionContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _context.SupplyerTable.ToListAsync());
        }

        //create action start from here
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Supplyer supplyer)
        {
            if(!ModelState.IsValid)
            {
                return View(supplyer);
            }
            var data = await _context.SupplyerTable.ToListAsync();
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].SupplyerName == supplyer.SupplyerName && data[i].SupplyerPhone == supplyer.SupplyerPhone)
                {
                    var ErrorMessage = new SupportClassErrorView()
                    {
                        IsError = true,
                        ErrorType = "Duplicate Value",
                        ErrorMessage = "Suppler is already exits"
                    };
                    return RedirectToAction("Create", ErrorMessage);
                }
            }
            await _context.SupplyerTable.AddAsync(supplyer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create(SupportClassErrorView error)
        {
            TempData["errormessage"] = error;
            return View();
        }
        //create action ends here

        //edit action start from here
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _context.SupplyerTable.FindAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Supplyer supplyer)
        {
            if(!ModelState.IsValid)
            {
                return View(supplyer);
            }
            _context.Entry(supplyer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //edit action ends here

        //delete action start from here
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _context.SupplyerTable.FindAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Supplyer supplyer)
        {
            _context.SupplyerTable.Remove(supplyer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //delete action ends here
    }
}
