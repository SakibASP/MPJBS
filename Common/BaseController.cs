using MPJBS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MPJBS.Common
{
    [Authorize]
    public class BaseController : Controller
    {
        public int? MemberId { get; set; }
        public string? UserId { get; set; }
        private readonly ApplicationDbContext _context = new();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext.User.Identity!.IsAuthenticated)
            {
                var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                UserId = CurrentUserId;
                MemberId = _context.Users.Find(CurrentUserId)?.MemberId;
            }
        }
    }
}
