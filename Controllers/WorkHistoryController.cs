using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPJBS.Common;
using MPJBS.Data;
using MPJBS.Models;

namespace MPJBS.Controllers
{
    public class WorkHistoryController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public WorkHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: WorkHistoryController
        public async Task<IActionResult> Index()
        {
            var model = await _context.WorkHistory.ToListAsync();   
            return View(model);
        }


        // GET: WorkHistoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkHistoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkHistory workHistory)
        {
            try
            {
                var count = _context.WorkHistory.Count() + 1;
                workHistory.CreatedBy = User.Identity?.Name;
                workHistory.WorkCode = "D-" + count;
                _context.WorkHistory.Add(workHistory);  
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessMessage;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                return View(workHistory);
            }
        }

        // GET: WorkHistoryController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workHistory = await _context.WorkHistory.FindAsync(id);
            if (workHistory == null)
            {
                return NotFound();
            }
            return View(workHistory);
        }

        // POST: WorkHistoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkHistory workHistory)
        {
            try
            {
                workHistory.ModifiedDate = DateTime.Now;
                workHistory.ModifiedBy = User.Identity!.Name;
                workHistory.WorkCode = "D-1";
                _context.Update(workHistory);
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessMessage;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
                return View(workHistory);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var workHistory = await _context.WorkHistory.FindAsync(id);
                if (workHistory == null)
                {
                    return NotFound();
                }
                _context.WorkHistory.Remove(workHistory);
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
