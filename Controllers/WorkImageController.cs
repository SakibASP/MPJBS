using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MPJBS.Common;
using MPJBS.Data;
using MPJBS.Models;
using System.Security.AccessControl;

namespace MPJBS.Controllers
{
    public class WorkImageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        // Specify the time zone for Bangladesh
        private static readonly TimeZoneInfo bdTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");

        public WorkImageController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: WorkImageController
        public async Task<IActionResult> Index()
        {
            var model = await _context.WorkImage.Include(x=>x.WorkHistory_).ToListAsync();
            return View(model);
        }


        // GET: WorkImageController/Create
        public IActionResult Create()
        {
            ViewData["WorkId"] = new SelectList(_context.WorkHistory, "Id", "WorkCode");
            return View();
        }

        // POST: WorkImageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkImage workImage)
        {
            try
            {
                IFormFile? img = Request.Form.Files.FirstOrDefault();
                if(img is not null)
                {
                    const string folderName = "WorkImages";
                    string? directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                    // Check if the directory exists; if not, create it
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var imageName = Guid.NewGuid().ToString() + "_" + workImage.ImageName + Path.GetExtension(img.FileName);
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, imageName);
                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                        workImage.CreatedBy = User.Identity?.Name;
                        workImage.ImagePath = uploadPath;
                        workImage.ImageName = imageName;
                        _context.WorkImage.Add(workImage);
                        await _context.SaveChangesAsync();
                        TempData[Constants.Success] = Constants.SuccessMessage;
                    }
                }
                else
                {
                    TempData[Constants.Error] = "No image was selected!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                TempData[Constants.Error] = Constants.ErrorMessage;
                return View();
            }
        }

        // GET: WorkImageController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // POST: WorkImageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkImage workImage)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: WorkImageController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = _context.WorkImage.Find(id);
                if (model == null)
                {
                    return NotFound();
                }
                _context.WorkImage.Remove(model);
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessRemovedMessage;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
