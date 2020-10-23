namespace TraceYourLife.GUI.MenuItems
{
    public enum MenuItemType
    {
        Greetings,
        Login,
        Settings,
        Chart
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
