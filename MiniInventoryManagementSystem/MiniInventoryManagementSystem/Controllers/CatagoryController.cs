﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.DbCon;
using MiniInventoryManagementSystem.Models;
using MiniInventoryManagementSystem.Models.SupportClass;

namespace MiniInventoryManagementSystem.Controllers
{
    public class CatagoryController : Controller
    {
        private readonly DbConnectionContext _context;

        public CatagoryController(DbConnectionContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _context.CatagoryTable.ToListAsync());
        }

        public ActionResult Create()
        {
            ViewBag.IsError = false;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Catagory catagory)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var data = await _context.CatagoryTable.ToListAsync();
            for(int i=0; i<data.Count; i++)
            {
                if (data[i].CatagoryName == catagory.CatagoryName)
                {
                    ViewBag.IsError = true;
                    ViewBag.errorType = "Duplicate Entry";
                    ViewBag.errorMassage = "Catagory Name Is Already Available";
                    return View(catagory);
                }
            }
            await _context.CatagoryTable.AddAsync(catagory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            return View(await _context.CatagoryTable.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Catagory catagory)
        {
            if (!ModelState.IsValid)
            {
                return View(catagory);
            }
            _context.Entry(catagory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            return View(await _context.CatagoryTable.FindAsync(id));
        }

        [HttpPost]
        public ActionResult Delete(Catagory catagory)
        {
            _context.CatagoryTable.Remove(catagory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
