using MPJBS.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table(nameof(Collection))]
    public class Collection : ModelBase
    {
        [Required]
        [DisplayName("সদস্যের নাম")]
        public int? MemberId { get; set; }
        [Required]
        [DisplayName("জমার তারিখ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM-yyyy}")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CollectionDate { get; set; }
        [Required]
        [DisplayName("চাঁদার পরিমাণ")]
        public double? PayableAmount { get; set; } = 0;
        [Required]
        [DisplayName("জমার পরিমাণ")]
        public double? PaidAmount { get; set; } = 0;
        [Required]
        [DisplayName("বাকির পরিমাণ")]
        public double? DueAmount { get { return PayableAmount - PaidAmount; }  }
        [ForeignKey(nameof(MemberId))]
        public virtual Members? Members { get; set; }
    }
}
