using OxyPlot;
using OxyPlot.Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceYourLife.Domain.Interfaces;
using TraceYourLife.Domain.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CycleChartPage : ContentPage
    {
        private readonly IPerson person;
        private TemperaturePerDayManager cycleHandler;

        public CycleChartPage(IPerson person)
        {
            if (PopupNavigation.Instance.PopupStack.Any())
                PopupNavigation.Instance.PopAsync();
            cycleHandler = new TemperaturePerDayManager(person);
            InitializeComponent();
            this.person = person;

            var bgImage = new Image()
            {
                Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("meer.jpg") : ImageSource.FromFile("meer.jpg")
            };
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            var absLayout = new AbsoluteLayout()
            {
                Children = { { bgImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional },
                    { layout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional }
                }
            };
            this.Content = absLayout;

            cycleHandler.CreateLineChart("Zyklus");
            PlotView view = GlobalGUISettings.CreatePlotModelCycle(cycleHandler);

            Button buttonInsertNewData = GlobalGUISettings.CreateButton("Neue Daten eingeben!");
            buttonInsertNewData.Clicked += ButtonInsertNewData_Clicked;

            layout.Children.Add(view);
            layout.Children.Add(buttonInsertNewData);
            layout.Padding = new Thickness(5, 10);
        }

        private async void ButtonInsertNewData_Clicked(object sender, EventArgs e)
        {
            var informationMessage = "Neue Daten eingeben";
            await Navigation.PushPopupAsync(new CycleChartDataPopupPage(person, informationMessage));
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new SettingsPage(person));
            return true;
        }
    }
}