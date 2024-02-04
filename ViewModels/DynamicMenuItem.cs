using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MPJBS.ViewModels
{
    public class DynamicMenuItem
    {
        public int? MID { get; set; }
        public string? MenuName { get; set; }
        public string? MenuURL { get; set; }
        public int? MenuParentID { get; set; }

    }
}
