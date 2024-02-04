using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MPJBS.Models.CustomIdentity
{
    public class ApplicationUser : IdentityUser
    {
        public int? UsernameChangeLimit { get; set; } = 10;
        public byte[]? ProfilePicture { get; set; }

        [Display(Name = "Employee")]
        public int? MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public virtual Members? Members { get; set; }
    }
}
