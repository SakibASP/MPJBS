using MPJBS.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table(nameof(Members))]
    public class Members : ModelBase
    {
        [Required]
        [DisplayName("নাম")]
        public string? Name { get; set; }
        [DisplayName("বয়স")]
        public int? Age { get; set; }
        [Required]
        [DisplayName("মোবাইল ১")]
        public string? Mobile { get; set; }
        [DisplayName("মোবাইল ২")]
        public string? Mobile_2 { get; set; }
        [DisplayName("রক্তের গ্রুপ")]
        public string? Blood_Group { get; set; }
        [Required]
        [DisplayName("চাঁদার পরিমান")]
        public double? Amount { get; set; }
        [Required]
        [DisplayName("পদবি")]
        public int? TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        public virtual MemberTypes? MemberTypes { get; set; }
    }
}
