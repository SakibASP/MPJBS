using MPJBS.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table(nameof(PaymentMethod))]
    public class PaymentMethod : ModelBase
    {
        public string? MethodName { get; set; }

        public string? MethodNo { get; set; }
    }
}
