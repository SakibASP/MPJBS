using MPJBS.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table(nameof(WorkHistory))]
    public class WorkHistory : ModelBase
    {
        public string? Title { get; set; }
        public string? WorkCode { get; set; }
        public string? Details { get; set; }
    }
}
