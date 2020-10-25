namespace TraceYourLife.GUI.MenuItems
{
    public enum MenuItemType
    {
        Start,
        Login,
        UserData,
        Chart
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
