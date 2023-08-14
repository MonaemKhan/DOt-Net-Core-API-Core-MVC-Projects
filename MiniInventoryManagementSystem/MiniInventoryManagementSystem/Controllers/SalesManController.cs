using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.DbCon;
using MiniInventoryManagementSystem.Models;
using System.Data.Common;

namespace MiniInventoryManagementSystem.Controllers
{
    public class SalesManController : Controller
    {
        private readonly DbConnectionContext _context;
        public SalesManController(DbConnectionContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _context.SalesManTable.ToListAsync());
        }

        //create action start from here
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(SalesMan salesman)
        {
            if(!ModelState.IsValid)
            {
                return View(salesman);
            }
            await _context.SalesManTable.AddAsync(salesman);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //create action ends here

        //Edit action start from here
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _context.SalesManTable.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SalesMan salesman)
        {
            if(!ModelState.IsValid)
            {
                return View(salesman);
            }
            _context.Entry(salesman).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //Edit action ends here

        //Details action Start from here
        public async Task<ActionResult> Details(int id)
        {
            return View(await _context.SalesManTable.FindAsync(id));
        }
        //Details Action ends here

        //Delete Action Start From Here
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _context.SalesManTable.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(SalesMan salesman)
        {
            _context.SalesManTable.Remove(salesman);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
