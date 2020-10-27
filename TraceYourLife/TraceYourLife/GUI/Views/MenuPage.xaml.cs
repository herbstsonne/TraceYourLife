using System.Collections.Generic;
using System.ComponentModel;
using TraceYourLife.GUI.MenuItems;
using Xamarin.Forms;

namespace TraceYourLife.GUI.Views
{
    [DesignTimeVisible(true)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        readonly List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Start, Title="Trace your life" },
                new HomeMenuItem {Id = MenuItemType.UserData, Title="Persönliche Daten" },
                new HomeMenuItem {Id = MenuItemType.Chart, Title="Basaltemperatur verwalten" },
                new HomeMenuItem {Id = MenuItemType.Logout, Title="Logout" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}