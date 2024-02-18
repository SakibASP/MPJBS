using Microsoft.AspNetCore.Mvc;
using MPJBS.Common;
using MPJBS.Data;
using MPJBS.ViewModels;

namespace MPJBS.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _hostingEnvironment;

        public ReportController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CollectionHistory(ReportParamViewModel reportParam)
        {
            ViewBag.parameterSettings = new BoldReports.Models.ReportViewer.ParameterSettings();
            var parameters = Utility.GenerateReportViewerParams(reportParam);
            ViewBag.parameters = parameters;
            reportParam.ReportFileName = "Collection.rdl";
            ViewBag.ReportFileName = reportParam.ReportFileName;
            return View(reportParam);
        }
    }
}
