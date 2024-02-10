using MPJBS.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table(nameof(WorkImage))]
    public class WorkImage : ModelBase
    {
        [Required]
        public int WorkId { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageName { get; set; }
        public bool IsCover { get; set; }
        [ForeignKey(nameof(WorkId))]
        public virtual WorkHistory? WorkHistory_ { get; set; }
    }
}
