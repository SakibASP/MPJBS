using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPJBS.Data;
using MPJBS.Models;
using MPJBS.ViewModels;
using System.Diagnostics;
using System.Text.Json;

namespace MPJBS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            //// Get client's IP address Ex:  "203.188.241.51"; 
            //var ipAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress;

            //// Make request to ip-api.com API
            //var client = _httpClientFactory.CreateClient();
            //var response = await client.GetAsync($"http://ip-api.com/json/{ipAddress}?fields=city,country,lat,lon");

            //if (response.IsSuccessStatusCode)
            //{
            //    // Parse JSON response
            //    var content = await response.Content.ReadAsStringAsync();
            //    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(content)!;

            //    if (result != null)
            //    {
            //        // Extract location information
            //        var city = result.city;
            //        var country = result.country;
            //        var latitude = result.lat;
            //        var longitude = result.lon;
            //    }
            //}
            //else
            //{
            //    // Handle error
            //    //return StatusCode((int)response.StatusCode);
            //}

            var model = await (from m in _context.WorkImage.Where(x=>x.IsCover)
                         join f in _context.WorkHistory on m.WorkId equals f.Id
                         select new WorkViewModel
                         {
                             WorkId = f.Id,
                             Title= f.Title,
                             Details = f.Details,
                             Mentions = f.Mentions,
                             ImageName = m.ImageName
                         }).ToListAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetMemberInfo(int? memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member is null)
            {
                return Json(null);
            }
            return Json(member, new JsonSerializerOptions());
        }

        [HttpGet]
        public async Task<JsonResult> GetIncomeExpense()
        {
            var income = await _context.Collection.SumAsync(x => x.PaidAmount);
            var expense = await _context.Expense.SumAsync(x => x.Amount);
            Dictionary<string, double?> incomeExpense = new()
            {
                { "Income", income },
                { "Expense", expense }
            };
            if (incomeExpense is null) return Json(null);
            return Json(incomeExpense);
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
