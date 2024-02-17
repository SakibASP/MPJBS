using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPJBS.Common;
using MPJBS.Data;
using MPJBS.Models;

namespace MPJBS.Controllers
{
    public class PaymentMethodController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public PaymentMethodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentMethod
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentMethod.ToListAsync());
        }

        // GET: PaymentMethod/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethod
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // GET: PaymentMethod/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethod/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentMethod paymentMethod)
        {
            try 
            {
                paymentMethod.CreatedBy = User.Identity?.Name;
                _context.Add(paymentMethod);
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessMessage;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }
            return View(paymentMethod);
        }

        // GET: PaymentMethod/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethod.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);
        }

        // POST: PaymentMethod/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return NotFound();
            }
            try
            {
                paymentMethod.ModifiedBy = User.Identity?.Name;
                paymentMethod.ModifiedDate = DateTime.Now;
                _context.Update(paymentMethod);
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessMessage;
                return RedirectToAction(nameof(Index));

            }
            catch 
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }

            return View(paymentMethod);
        }

        // GET: PaymentMethod/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethod
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // POST: PaymentMethod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var paymentMethod = await _context.PaymentMethod.FindAsync(id);
                if (paymentMethod != null)
                {
                    _context.PaymentMethod.Remove(paymentMethod);
                }

                await _context.SaveChangesAsync();
                TempData[Constants.Success] = Constants.SuccessMessage;
            }
            catch
            {
                TempData[Constants.Error] = Constants.ErrorMessage;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentMethodExists(int id)
        {
            return _context.PaymentMethod.Any(e => e.Id == id);
        }
    }
}
