using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MPJBS.Common;
using MPJBS.Data;
using MPJBS.Models;

namespace MPJBS.Controllers
{
    public class MembersController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Members
        public async Task<IActionResult> Index()
        {
            var model = _context.Members.Include(m => m.MemberTypes);
            return View(await model.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .Include(m => m.MemberTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }

            return View(members);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.MemberTypes, "Id", "TypeName");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Members members)
        {

            try
            {
                members.CreatedBy = User.Identity!.Name;
                _context.Add(members);
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessMessage;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }
            
            ViewData["TypeId"] = new SelectList(_context.MemberTypes, "Id", "TypeName", members.TypeId);
            return View(members);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members.FindAsync(id);
            if (members == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.MemberTypes, "Id", "TypeName", members.TypeId);
            return View(members);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Members members)
        {
            if (id != members.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    members.ModifiedDate = DateTime.Now;
                    members.ModifiedBy = User.Identity!.Name;
                    _context.Update(members);
                    await _context.SaveChangesAsync();
                    TempData[Constants.Success] = Constants.SuccessMessage;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembersExists(members.Id))
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
            ViewData["TypeId"] = new SelectList(_context.MemberTypes, "Id", "TypeName", members.TypeId);
            return View(members);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .Include(m => m.MemberTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }

            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var members = await _context.Members.FindAsync(id);
                if (members != null)
                {
                    _context.Members.Remove(members);
                }

                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessRemovedMessage;
            }
            catch (Exception ex)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MembersExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
