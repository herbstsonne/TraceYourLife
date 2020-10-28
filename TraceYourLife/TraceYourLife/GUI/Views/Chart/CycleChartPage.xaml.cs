using System;
using OxyPlot.Xamarin.Forms;
using TraceYourLife.Domain.Manager;
using TraceYourLife.GUI.Views.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views.Chart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CycleChartPage : ContentPage, IInitializePage
    {
        private ChartDrawer _chartDrawer;
        private TemperaturePerDayChartManager _temperaturePerDayChartManager;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _chartDrawer = new ChartDrawer();
            _temperaturePerDayChartManager = new TemperaturePerDayChartManager();
            SetPageParameters();
        }

        public void ReloadPage()
        {
            _chartDrawer = new ChartDrawer();
            _temperaturePerDayChartManager = new TemperaturePerDayChartManager();
            _chartDrawer.FillCyclePointList(_temperaturePerDayChartManager.RetrieveCycleOf);
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
            var bgImage = GuiElementsFactory.CreateImage("himmel.jpg");
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            var absLayout = new AbsoluteLayout()
            {
                Children = { { bgImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional },
                    { layout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional }
                }
            };
            this.Content = absLayout;

            _chartDrawer.CreateLineChart("Zyklus", _temperaturePerDayChartManager.RetrieveCycleOf);
            PlotView view = GuiElementsFactory.CreatePlotModelCycle(_chartDrawer);

            var buttonInfo = GuiElementsFactory.CreateButton("Info");
            buttonInfo.Clicked += ButtonInfo_Clicked;
            Button buttonInsertNewData = GuiElementsFactory.CreateButton("Wert eingeben");
            buttonInsertNewData.Clicked += ButtonInsertNewData_Clicked;

            layout.Children.Add(buttonInfo);
            layout.Children.Add(view);
            layout.Children.Add(buttonInsertNewData);
            layout.Padding = new Thickness(5, 10);
        }

        private async void ButtonInfo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BasaltemperatureInfoPopupPage());
            ReloadPage();
        }

        private async void ButtonInsertNewData_Clicked(object sender, EventArgs e)
        {
            var informationMessage = "Neue Daten eingeben";
            await Navigation.PushModalAsync(new CycleChartDataPopupPage(informationMessage));
            ReloadPage();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new NavigationPage(new PersonalDataPage()));
            return true;
        }
    }
}