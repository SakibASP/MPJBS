namespace MPJBS.ViewModels
{
    public class MenuToRoleViewModel
    {
        public List<MenuSelection> MenuSelections { set; get; }
        public string RoleId { set; get; }

        public List<int> MenuParentIds { get; internal set; }
        public MenuToRoleViewModel()
        {
            MenuSelections = new List<MenuSelection>();
        }
    }
}
