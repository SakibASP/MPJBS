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

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var images = await _context.WorkImage.Where(x=>x.WorkId == id).ToListAsync();
            var workHistory = await _context.WorkHistory.FindAsync(id);
            ViewData["Images"] = images;
            return View(workHistory);
        }


        // GET: WorkImageController/Create
        public IActionResult Create()
        {
            ViewData["WorkId"] = new SelectList(_context.WorkHistory, "Id", "WorkCode");
            ViewData["IsCover"] = new SelectList(StaticDropdowns.YesNoList(), "Value", "Text");
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
                    
                    //saving resized image
                    var _path = Utility.SaveImage(img, uploadPath);
                    if(_path is not null)
                    {
                        workImage.CreatedBy = User.Identity?.Name;
                        workImage.ImagePath = uploadPath;
                        workImage.ImageName = imageName;
                        workImage.IsCover = Convert.ToBoolean(workImage.IsCover);
                        if(Convert.ToBoolean(workImage.IsCover))
                        {
                            var images = _context.WorkImage.Where(x=>x.WorkId == workImage.WorkId && x.IsCover);
                            if (images.Any())
                            {
                                foreach (var image in images)
                                {
                                    image.IsCover = false;
                                }
                                _context.UpdateRange(images);
                                await _context.SaveChangesAsync();
                            }
                        }
                        _context.WorkImage.Add(workImage);
                        await _context.SaveChangesAsync();
                        TempData[Constants.Success] = Constants.SuccessMessage;
                    }

                    //saving raw image
                    //using (var stream = new FileStream(uploadPath, FileMode.Create))
                    //{
                    //    await img.CopyToAsync(stream);
                    //    workImage.CreatedBy = User.Identity?.Name;
                    //    workImage.ImagePath = uploadPath;
                    //    workImage.ImageName = imageName;
                    //    _context.WorkImage.Add(workImage);
                    //    await _context.SaveChangesAsync();
                    //    TempData[Constants.Success] = Constants.SuccessMessage;
                    //}
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
            }
            ViewData["WorkId"] = new SelectList(_context.WorkHistory, "Id", "WorkCode", workImage.WorkId);
            ViewData["IsCover"] = new SelectList(StaticDropdowns.YesNoList(), "Value", "Text", workImage.IsCover);
            return View(workImage);
        }

        // GET: WorkImageController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workImage = await _context.WorkImage.FindAsync(id);
            if (workImage == null)
            {
                return NotFound();
            }
            ViewData["WorkId"] = new SelectList(_context.WorkHistory, "Id", "WorkCode", workImage.WorkId);
            ViewData["IsCover"] = new SelectList(StaticDropdowns.YesNoList(), "Value", "Text", workImage.IsCover);
            return View(workImage);
        }

        // POST: WorkImageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, WorkImage workImage)
        {
            if (id != workImage.Id)
            {
                return NotFound();
            }
            try
            {
                workImage.ModifiedBy = User.Identity?.Name;
                workImage.ModifiedDate = DateTime.Now;
                if (Convert.ToBoolean(workImage.IsCover))
                {
                    var images = _context.WorkImage.Where(x => x.WorkId == workImage.WorkId && x.IsCover);
                    if (images.Any())
                    {
                        foreach (var image in images)
                        {
                            image.IsCover = false;
                        }
                        _context.UpdateRange(images);
                        await _context.SaveChangesAsync();
                    }
                }
                _context.Update(workImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }
            ViewData["WorkId"] = new SelectList(_context.WorkHistory, "Id", "WorkCode", workImage.WorkId);
            ViewData["IsCover"] = new SelectList(StaticDropdowns.YesNoList(), "Value", "Text", workImage.IsCover);
            return View();
        }

        // POST: WorkImageController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _context.WorkImage.FindAsync(id);
                if (model == null)
                {
                    return NotFound();
                }

                // Check if the file exists before attempting to delete it
                if (System.IO.File.Exists(model.ImagePath))                 
                    System.IO.File.Delete(model.ImagePath);

                _context.WorkImage.Remove(model);
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessRemovedMessage;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                return View();
            }
        }
    }
}
