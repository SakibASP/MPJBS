using Microsoft.AspNetCore.Mvc.Rendering;

namespace MPJBS.Common
{
    public static class StaticDropdowns
    {
        public static List<SelectListItem> YesNoList()
        {
            var list = new List<SelectListItem>
            {
                new() { Value = "true", Text = "YES" },
                new() { Value = "false", Text = "NO" }
            };

            return list;
        }
    }
}
