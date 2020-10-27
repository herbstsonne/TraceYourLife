using System;
using System.Threading.Tasks;
using OxyPlot.Xamarin.Forms;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Manager;
using TraceYourLife.Domain.Manager.Interfaces;
using TraceYourLife.GUI.Views.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views.Chart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CycleChartPage : ContentPage, IInitializePage
    {
        private IPersonManager _personManager;
        private ChartDrawer _chartDrawer;
        private TemperaturePerDayChartManager _temperaturePerDayChartManager;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _personManager = new PersonManager();
            _chartDrawer = new ChartDrawer();
            _temperaturePerDayChartManager = new TemperaturePerDayChartManager();
            SetPageParameters();
        }

        public async Task ReloadPage()
        {
            _chartDrawer.FillCyclePointList(_temperaturePerDayChartManager.RetrieveCycleOf);
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
            var bgImage = new Image()
            {
                Source = "himmel.jpg",
                Aspect = Aspect.Fill
            };
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

            Button buttonInsertNewData = GuiElementsFactory.CreateButton("Wert eingeben");
            buttonInsertNewData.Clicked += ButtonInsertNewData_Clicked;

            layout.Children.Add(view);
            layout.Children.Add(buttonInsertNewData);
            layout.Padding = new Thickness(5, 10);
        }

        private async void ButtonInsertNewData_Clicked(object sender, EventArgs e)
        {
            var informationMessage = "Neue Daten eingeben";
            await Navigation.PushModalAsync(new CycleChartDataPopupPage(informationMessage));
            await ReloadPage();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new NavigationPage(new PersonalDataPage()));
            return true;
        }
    }
}