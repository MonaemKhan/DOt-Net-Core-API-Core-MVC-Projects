using EmployeeDetails.DbCon;
using EmployeeDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace EmployeeDetails.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DbConnectionContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(DbConnectionContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ActionResult> Index()
        {
            var data = await _context.EmployeeTable.Include(x=>x.Employee_country).Include(x=>x.Employee_State).Include(x=>x.Employee_City).ToListAsync();
            return View(data);
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Country = await _context.CountryTable.ToListAsync();
            ViewBag.State = await _context.StateTable.ToListAsync();
            ViewBag.City = await _context.CityTable.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Employee emp)
        {

            if (ModelState.IsValid)
            {
                if (emp.ImageFile != null && emp.ImageFile.Length > 0)
                {
                    var filename = Path.GetFileNameWithoutExtension(emp.ImageFile.FileName);
                    var extention = Path.GetExtension(emp.ImageFile.FileName);
                    emp.ImageName = filename = emp.Name.Split(" ")[1] + " "+emp.EmployeeId + extention;
                    var path = Path.Combine(_webHostEnvironment.WebRootPath + "/UserImages/" + filename);
                    using (var steam = new FileStream(path, FileMode.Create))
                    {
                        await emp.ImageFile.CopyToAsync(steam);
                    }
                }
                await _context.AddAsync(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Country = await _context.CountryTable.ToListAsync();
            ViewBag.State = await _context.StateTable.ToListAsync();
            ViewBag.City = await _context.CityTable.ToListAsync();
            return View(emp);
        }
        public async Task<ActionResult> GetStateByCountryId(int conId)
        {
            var stateList = await _context.StateTable.Where(x=>x.State_CountryId==conId).ToListAsync();
            return Json(stateList);
        }

        public async Task<ActionResult> GetCityByStateId(int staId)
        {
            var cityList = await _context.CityTable.Where(x => x.City_StateId == staId).ToListAsync();
            return Json(cityList);
        }

        //Edit Action
        public async Task<ActionResult> Edit(int Id)
        {
            ViewBag.Country = await _context.CountryTable.ToListAsync();
            ViewBag.State = await _context.StateTable.ToListAsync();
            ViewBag.City = await _context.CityTable.ToListAsync();
            return View(await _context.EmployeeTable.FindAsync(Id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                if (emp.ImageFile != null && emp.ImageFile.Length > 0)
                {
                    var de_path = Path.Combine(_webHostEnvironment.WebRootPath + "/UserImages/" + emp.ImageName);
                    if(System.IO.File.Exists(de_path))
                    {
                        System.IO.File.Delete(de_path);
                    }
                    var filename = Path.GetFileNameWithoutExtension(emp.ImageFile.FileName);
                    var extention = Path.GetExtension(emp.ImageFile.FileName);
                    emp.ImageName = filename = emp.Name.Split(" ")[1] +" "+emp.EmployeeId+" " + extention;
                    var path = Path.Combine(_webHostEnvironment.WebRootPath + "/UserImages/" + filename);
                    using (var steam = new FileStream(path, FileMode.Create))
                    {
                        await emp.ImageFile.CopyToAsync(steam);
                    }
                }
                _context.Entry(emp).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Country = await _context.CountryTable.ToListAsync();
            ViewBag.State = await _context.StateTable.ToListAsync();
            ViewBag.City = await _context.CityTable.ToListAsync();
            return View(emp);
        }

        //Details Action
        public async Task<ActionResult> Details(int Id)
        {
            var data = await _context.EmployeeTable.
                Include(x => x.Employee_country).
                Include(x => x.Employee_State).
                Include(x => x.Employee_City).
                FirstOrDefaultAsync(x=>x.EmployeeId == Id);
            
            return View(data);
        }

        //Delete Action
        public async Task<ActionResult> Delete(int Id)
        {
            var data = await _context.EmployeeTable.
                Include(x => x.Employee_country).
                Include(x => x.Employee_State).
                Include(x => x.Employee_City).
                FirstOrDefaultAsync(x => x.EmployeeId == Id);

            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Employee emp)
        {
            if(emp.ImageName != null)
            {
                var de_path = Path.Combine(_webHostEnvironment.WebRootPath + "/UserImages/" + emp.ImageName);
                if (System.IO.File.Exists(de_path))
                {
                    System.IO.File.Delete(de_path);
                }
            }
            _context.EmployeeTable.Remove(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
