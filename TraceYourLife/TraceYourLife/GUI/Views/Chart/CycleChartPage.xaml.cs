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
        Label labelOvulationInfo;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _chartDrawer = new ChartDrawer();
            _temperaturePerDayChartManager = new TemperaturePerDayChartManager();
            SetPageParameters();
        }

        public void ReloadPage()
        {
            //_chartDrawer = new ChartDrawer();
            //_temperaturePerDayChartManager = new TemperaturePerDayChartManager();
            //SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
            var bgImage = GuiElementsFactory.CreateImage("greenpastell.jpg");
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            var absLayout = new AbsoluteLayout()
            {
                Children = { { bgImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional },
                    { layout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional }
                }
            };
            this.Content = absLayout;

            _chartDrawer.CreateLineChart("Zyklus", _temperaturePerDayChartManager.GetBasalTempData, _temperaturePerDayChartManager.GetCoverlineData);
            PlotView view = GuiElementsFactory.CreatePlotModelCycle(_chartDrawer);

            var buttonInfo = GuiElementsFactory.CreateButtonInfo("i");
            buttonInfo.Clicked += ButtonInfo_Clicked;
            Button buttonInsertNewData = GuiElementsFactory.CreateButton("Wert eingeben");
            buttonInsertNewData.Clicked += ButtonInsertNewData_Clicked;
            labelOvulationInfo = GuiElementsFactory.CreateLabel(GetLabelOvulationText(), 15);

            var gridButtonInfo = new Grid();
            gridButtonInfo.Children.Add(buttonInfo, 2, 0);

            layout.Children.Add(labelOvulationInfo);
            layout.Children.Add(gridButtonInfo);
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

        private string GetLabelOvulationText()
        {
            if (_temperaturePerDayChartManager.OvulationEntry != null)
            {
                var ovuDate = _temperaturePerDayChartManager.OvulationEntry.Date.ToShortDateString();
                return String.Format("Ihr Eisprung hat am {0} stattgefunden.", ovuDate);
            }
            return "";
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new NavigationPage(new PersonalDataPage()));
            return true;
        }
    }
}