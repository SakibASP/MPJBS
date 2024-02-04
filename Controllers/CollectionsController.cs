using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MPJBS.Data;
using MPJBS.Models;
using MPJBS.Common;

namespace MPJBS.Controllers
{

    public class CollectionsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CollectionsController(ApplicationDbContext context){
            _context = context;
        }
        // GET: Collections
        public async Task<IActionResult> Index()
        {
            var model = _context.Collection.Include(c => c.Members);
            return View(await model.ToListAsync());
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .Include(c => c.Members)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name");
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Collection collection)
        {
            try
            {
                collection.CreatedBy = User.Identity!.Name;
                _context.Add(collection);
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessMessage;
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name", collection.MemberId);
            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name", collection.MemberId);
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Collection collection)
        {
            if (id != collection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    collection.ModifiedDate = DateTime.Now;
                    collection.ModifiedBy = User.Identity!.Name;
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    TempData[Constants.Error] = Constants.ErrorMessage;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Name", collection.MemberId);
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .Include(c => c.Members)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var collection = await _context.Collection.FindAsync(id);
                if (collection != null)
                {
                    _context.Collection.Remove(collection);
                }

                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id)
        {
            return _context.Collection.Any(e => e.Id == id);
        }
    }
}
