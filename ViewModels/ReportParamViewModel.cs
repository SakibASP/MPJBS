using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace MPJBS.ViewModels
{
    public class ReportParamViewModel
    {
        public string ReportName { get; set; } = "Empty";
        public string ReportFileName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
    }
}
