using MPJBS.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table(nameof(Expense))]
    public class Expense : ModelBase
    {
        [Required]
        [DisplayName("খরচের তারিখ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpenseDate { get; set; }
        [Required]
        [DisplayName("খরচের কারণ")]
        public string? ExpenseDetails { get; set; }
        [Required]
        [DisplayName("খরচের পরিমাণ")]
        public double Amount { get; set; }
        [Required]
        [DisplayName("নেতৃত্ব প্রদানকারী")]
        public int? MemberId { get; set; }
        [ForeignKey(nameof(MemberId))]
        public virtual Members? Members { get; set; }
    }
}
