using MPJBS.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table(nameof(MemberTypes))]
    public class MemberTypes : ModelBase
    {
        [Required]
        [DisplayName("পদবি")]
        public string? TypeName { get; set; }
    }
}
