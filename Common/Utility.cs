using MPJBS.ViewModels;

namespace MPJBS.Common
{
    public static class Utility
    {
        //for viewing report (Bold Report)
        public static List<BoldReports.Models.ReportViewer.ReportParameter> GenerateReportViewerParams(ReportParamViewModel reportParam)
        {
            List<BoldReports.Models.ReportViewer.ReportParameter> parameters = new()
            {
                new()
                {
                    Name = "StartDate",
                    Values = new List<string>() { reportParam.StartDate.ToString()! }
                },
                new()
                {
                    Name = "EndDate",
                    Values = new List<string>() { reportParam.EndDate.ToString()! }
                }
            };
            return parameters;
        }
    }
}
