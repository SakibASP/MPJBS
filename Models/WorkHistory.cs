using MPJBS.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table(nameof(WorkHistory))]
    public class WorkHistory : ModelBase
    {
        [Required]
        public string? Title { get; set; }
        public string? WorkCode { get; set; }
        [Required]
        public string? Details { get; set; }
        [Required]
        public string? Mentions { get; set; }
    }
}
