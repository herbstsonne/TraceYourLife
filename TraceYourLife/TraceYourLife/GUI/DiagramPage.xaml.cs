using Microcharts;
using Microcharts.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DiagramPage : ContentPage
    {
        private readonly IPerson person;
        private TemperaturePerDayManager cycleHandler;

        public DiagramPage(IPerson person)
        {
            PopupNavigation.Instance.PopAsync();
            cycleHandler = new TemperaturePerDayManager(person);
            this.person = person;
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            this.Content = layout;

            ChartView view = new ChartView();
            view.Chart = new LineChart
            {
                LineMode = LineMode.None,
                LineSize = 8,
                BackgroundColor = SKColor.FromHsv(100, 100, 100)
            };
            view.WidthRequest = 250;
            view.HeightRequest = 250;
            view.VerticalOptions = LayoutOptions.CenterAndExpand;
            view.HorizontalOptions = LayoutOptions.CenterAndExpand;
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