using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table("MenuToRole")]
    public class MenuToRole
    {
        [Key]
        public int? Id { get; set; }
        public string RoleId { get; set; }
        public int? MenuId { get; set; }
        public bool? Active { get; set; }
        public bool? IsSelected { get; set; }
    }
}
