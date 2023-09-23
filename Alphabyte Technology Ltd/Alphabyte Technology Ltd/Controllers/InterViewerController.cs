using Alphabyte_Technology_Ltd_.DbConn;
using Alphabyte_Technology_Ltd_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Alphabyte_Technology_Ltd_.Controllers
{
    public class InterViewerController : Controller
    {
        private readonly DbConnectionClass _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public InterViewerController(DbConnectionClass context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.InterViewerDetailsTable.Include(x=>x.InterViewerDetails_Division).Include(x=>x.InterViewerDetails_Department).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Division = _context.DivisionTable.ToList();
            ViewBag.Department = _context.DepartmentTable.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InterViewerDetails ivd, IFormFile resumeFile)
        {
            if(resumeFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath; //it get the project environment
                string fileName = Path.GetFileNameWithoutExtension(resumeFile.FileName); //it get the file name from the input, that we take as input
                string extension = Path.GetExtension(resumeFile.FileName); // it get extension name of the file, that we take input 
                if (extension == ".pdf" || extension == ".docx")
                {
                    fileName = "File1" + extension;
                    string path = Path.Combine(wwwRootPath + "/ResumeUpload/", fileName); // combine path
                    int count = 2;
                    while (System.IO.File.Exists(path)) {
                        fileName = "File" + count + extension;
                        path = Path.Combine(wwwRootPath + "/ResumeUpload/", fileName);
                        count++;
                    }
                    ivd.ResumeFile = fileName;
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await resumeFile.CopyToAsync(fileStream); // now copy the images into the selected folder
                    }
                }
                else
                {
                    ViewBag.FileError = "File Type Error";
                    ViewBag.Division = _context.DivisionTable.ToList();
                    ViewBag.Department = _context.DepartmentTable.ToList();
                    return View(ivd);
                }
                
            }


                await _context.InterViewerDetailsTable.AddAsync(ivd);
                await _context.SaveChangesAsync();

           
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetDeptByDivId(int divId)
        {
            var data = await _context.DepartmentTable.Where(x => x.Department_DivisionId == divId).ToListAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string Id)
        {
            var data = await _context.InterViewerDetailsTable
                .Include(x => x.InterViewerDetails_Division)
                .Include(x => x.InterViewerDetails_Department)
                .Where(x => x.Id.Equals(Id))
                .ToListAsync();
            return View(data[0]);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var data = await _context.InterViewerDetailsTable
                .Include(x => x.InterViewerDetails_Division)
                .Include(x => x.InterViewerDetails_Department)
                .Where(x => x.Id.Equals(Id))
                .ToListAsync();
            return View(data[0]);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var data = await _context.InterViewerDetailsTable.FindAsync(Id);
            if(data == null)
            {
                return BadRequest(string.Empty);
            }
            if (System.IO.File.Exists(Path.Combine(_hostEnvironment.WebRootPath + "/ResumeUpload/", data.ResumeFile))) //check if it is exits
            {
                System.IO.File.Delete(Path.Combine(_hostEnvironment.WebRootPath + "/ResumeUpload/", data.ResumeFile)); //if exists then delete the file
            }
            _context.InterViewerDetailsTable.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var data = await _context.InterViewerDetailsTable
                .Include(x => x.InterViewerDetails_Division)
                .Include(x => x.InterViewerDetails_Department)
                .Where(x => x.Id.Equals(Id))
                .ToListAsync();
            ViewBag.Division = _context.DivisionTable.ToList();
            ViewBag.Department = _context.DepartmentTable.ToList();
            return View(data[0]);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InterViewerDetails ivd, IFormFile resume)
        {
            if (resume != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath; //it get the project environment
                string fileName = Path.GetFileNameWithoutExtension(resume.FileName); //it get the file name from the input, that we take as input
                string extension = Path.GetExtension(resume.FileName); // it get extension name of the file, that we take input 
                if (extension == ".pdf" || extension == ".docx")
                {
                    if (System.IO.File.Exists(Path.Combine(wwwRootPath + "/ResumeUpload/",ivd.ResumeFile))) //check if it is exits
                    {
                        System.IO.File.Delete(Path.Combine(wwwRootPath + "/ResumeUpload/", ivd.ResumeFile)); //if exists then delete the file
                    }
                    string path = Path.Combine(wwwRootPath + "/ResumeUpload/", ivd.ResumeFile); // combine path
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await resume.CopyToAsync(fileStream); // now copy the images into the selected folder
                    }
                }
                else
                {
                    ViewBag.FileError = "File Type Error";
                    ViewBag.Division = _context.DivisionTable.ToList();
                    ViewBag.Department = _context.DepartmentTable.ToList();
                    return View(ivd);
                }

            }
            _context.Entry(ivd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
