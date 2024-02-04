using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MPJBS.Models
{
    [Table("MenuItem")]
    public class MenuItem
    {
        [Key]
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public int? MenuParentId { get; set; }
        public bool? Active { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }
        [NotMapped]
        public virtual List<MenuItem> Children { get; set; }
    }
}
