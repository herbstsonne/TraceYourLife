namespace TraceYourLife.GUI.MenuItems
{
    public enum MenuItemType
    {
        Start,
        UserData,
        Chart,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
