using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using TraceYourLife.GUI.MenuItems;
using TraceYourLife.GUI.Views.Chart;
using TraceYourLife.GUI.Views.Interfaces;
using Xamarin.Forms;

namespace TraceYourLife.GUI.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Start, (NavigationPage)Detail);
            IsPresentedChanged += MainPage_IsPresentedChanged;
        }

        private void MainPage_IsPresentedChanged(object sender, EventArgs e)
        {
            //bug fix: workaround to reset PlotView before opening CycleChartPage again, how can I improve this?
            if (MenuPages.ContainsKey(2))
            {
                var layout = ((CycleChartPage)MenuPages[2].CurrentPage).Layout;
                if (layout?.Children != null && GuiElementsFactory.PlotView != null && layout.Children.Contains(GuiElementsFactory.PlotView))
                {
                    layout.Children.Remove(GuiElementsFactory.PlotView);
                }
            }
            GuiElementsFactory.PlotView = null;
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Start:
                        MenuPages.Add(id, new NavigationPage(new StartPage()));
                        break;
                    case (int)MenuItemType.UserData:
                        MenuPages.Add(id, new NavigationPage(new PersonalDataPage()));
                        break;
                    case (int)MenuItemType.Chart:
                        MenuPages.Add(id, new NavigationPage(new CycleChartPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        Application.Current.MainPage = new LoginPage();
                        return;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                var page = (IInitializePage) newPage.CurrentPage;
                page?.ReloadPage();
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);
                IsPresented = false;
            }
        }
    }
}