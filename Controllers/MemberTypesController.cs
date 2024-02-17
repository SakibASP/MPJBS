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
    public class MemberTypesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public MemberTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: MemberTypes
        public async Task<IActionResult> Index()
        {
            try
            {
                var value = _context.MemberTypes.ToList();
                var model = await _context.MemberTypes.ToListAsync();
                return View(model);
                
                
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        // GET: MemberTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberTypes = await _context.MemberTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberTypes == null)
            {
                return NotFound();
            }

            return View(memberTypes);
        }

        // GET: MemberTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberTypes memberTypes)
        {
            try
            {
                memberTypes.CreatedBy = User.Identity!.Name;
                _context.Add(memberTypes);
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessMessage;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }
            
            return View(memberTypes);
        }

        // GET: MemberTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberTypes = await _context.MemberTypes.FindAsync(id);
            if (memberTypes == null)
            {
                return NotFound();
            }
            return View(memberTypes);
        }

        // POST: MemberTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,MemberTypes memberTypes)
        {
            if (id != memberTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberTypes);
                    await _context.SaveChangesAsync();
                    TempData[Constants.Success] = Constants.SuccessMessage;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberTypesExists(memberTypes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exception ex)
                {
                    TempData[Constants.Error] = Constants.ErrorMessage;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(memberTypes);
        }

        // GET: MemberTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberTypes = await _context.MemberTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberTypes == null)
            {
                return NotFound();
            }

            return View(memberTypes);
        }

        // POST: MemberTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var memberTypes = await _context.MemberTypes.FindAsync(id);
                if (memberTypes != null)
                {
                    _context.MemberTypes.Remove(memberTypes);
                }
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessRemovedMessage;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MemberTypesExists(int id)
        {
            return _context.MemberTypes.Any(e => e.Id == id);
        }
    }
}
