using BoldReports.Web.ReportViewer;
using Microsoft.AspNetCore.Mvc;

namespace MPJBS.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReportViewerController : Controller, IReportController
    {
        Dictionary<string, object> jsonArray = null;
        // Report Viewer requires a memory cache to store the information of consecutive client requests 
        //and have the rendered Report Viewer information in the server.
        private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;

        // IWebHostEnvironment used with sample to get the application data from wwwroot.
        private IWebHostEnvironment _hostingEnvironment;

        // Post action to process the report from server-based JSON parameters and send the result back to the client.
        public ReportViewerController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
        IWebHostEnvironment hostingEnvironment)
        {
            _cache = memoryCache;
            _hostingEnvironment = hostingEnvironment;
        }

        // Post action to process the report from server-based JSON parameters and send the result back to the client.
        [HttpPost]
        public object PostReportAction([FromBody] Dictionary<string, object> jsonResult)
        {
            jsonArray = jsonResult;
            //Contains helper methods that help process a Post or Get request from the Report Viewer control and return the response to the Report Viewer control.
            return ReportHelper.ProcessReport(jsonResult, this, this._cache);
        }

        // Method will be called to initialize the report information to load the report with ReportHelper for processing.
        [NonAction]
        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            string basePath = _hostingEnvironment.WebRootPath;
            // Here, we have loaded the TransactionHistory.rdl report from the application folder wwwroot\Resources. The TransactionHistory.rdl file should be in the wwwroot\Resources application folder.
            FileStream inputStream = new FileStream(basePath
            + "\\RDL_Reports\\" + reportOption.ReportModel.ReportPath,
            FileMode.Open, FileAccess.Read);
            MemoryStream reportStream = new MemoryStream();
            inputStream.CopyTo(reportStream);
            reportStream.Position = 0;
            inputStream.Close();
            reportOption.ReportModel.Stream = reportStream;
        }

        // Method will be called when report is loaded internally to start to layout process with ReportHelper.
        [NonAction]
        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
            var reportParameters = ReportHelper.GetParameters(jsonArray, this, _cache);
            List<BoldReports.Web.ReportParameter> modifiedParameters = new();

           // ReportParamViewModel reportParam = new();
            if (reportParameters != null)
            {
                foreach (var rptParameter in reportParameters)
                {
                    // Hiding integer payment types
                    //if (rptParameter.Name.Equals(nameof(reportParam.PaymentType)) || rptParameter.Name.Equals(nameof(reportParam.SubPaymentType)))
                    //{
                    //    modifiedParameters.Add(new BoldReports.Web.ReportParameter()
                    //    {
                    //        Name = rptParameter.Name,
                    //        Hidden = true
                    //    });
                    //}
                    //showing other parameters field
                    modifiedParameters.Add(new BoldReports.Web.ReportParameter()
                    {
                        Name = rptParameter.Name,
                        Hidden = true
                        //Values = new List<string>() { null },
                    });
                }

                reportOption.ReportModel.Parameters = modifiedParameters;
            }
        }

        //Get action for getting resources from the report.
        [ActionName("GetResource")]
        [AcceptVerbs("GET")]
        // Method will be called from Report Viewer client to get the image src for the Image report item.
        public object GetResource(ReportResource resource)
        {
            return ReportHelper.GetResource(resource, this, _cache);
        }

        [HttpPost]
        public object PostFormReportAction()
        {
            return ReportHelper.ProcessReport(null, this, _cache);
        }
    }
}
