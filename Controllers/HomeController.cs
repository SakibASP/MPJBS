using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPJBS.Data;
using MPJBS.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MPJBS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetMemberInfo(int? memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member is null)
            {
                return Json(null);
            }
            return Json(member, new JsonSerializerOptions());
        }

        public async Task<IActionResult> Members()
        {
            var members = await _context.Members.Include(x=>x.MemberTypes).ToListAsync();
            return View(members);
        }

        public async Task<IActionResult> Collection()
        {
            var collection = await _context.Collection.Include(x=>x.Members).ToListAsync();
            return View(collection);
        }

        public async Task<IActionResult> Expense()
        {
            var expenses = await _context.Expense.Include(x => x.Members).ToListAsync();
            return View(expenses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
