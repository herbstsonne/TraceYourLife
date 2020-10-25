using System;
using System.Globalization;
using System.Threading.Tasks;
using OxyPlot.Xamarin.Forms;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Manager;
using TraceYourLife.GUI.Views.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views.Chart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CycleChartPage : ContentPage, IInitializePage
    {
        private IPerson _person;
        private PersonManager _personManager;
        private TemperaturePerDayChartManager _chartHandler;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _personManager = new PersonManager();
            _person = await _personManager.LoadFirstPerson();
            if (_person == null)
            {
                await Navigation.PushAsync(new NavigationPage(new SettingsPage()));
                return;
            }
            _chartHandler = new TemperaturePerDayChartManager(_person);
            SetPageParameters();
        }

        public async Task ReloadPage()
        {
            _person = _person ?? await _personManager.LoadFirstPerson();
            _chartHandler.RetrieveCycleOf();
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
            var bgImage = new Image()
            {
                Source = ImageSource.FromFile("himmel.jpg")
            };
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            var absLayout = new AbsoluteLayout()
            {
                Children = { { bgImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional },
                    { layout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional }
                }
            };
            this.Content = absLayout;

            var cycleHandler = new TemperaturePerDayChartManager(_person);
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
            var currentDate = DateTime.Now.ToShortDateString();
            string result = await DisplayPromptAsync(informationMessage, currentDate, 
                initialValue: "36,00", keyboard: Keyboard.Numeric);
            RenewCycleTable(result);
            await ReloadPage();
            //await Navigation.PushPopupAsync(new CycleChartDataPopupPage(_person, informationMessage));
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new NavigationPage(new SettingsPage()));
            return true;
        }

        private void RenewCycleTable(string result)
        {
            var decimalValue = decimal.Parse(result, new NumberFormatInfo() { NumberDecimalSeparator = "," });
            _chartHandler.UpdateCycleEntryTable(DateTime.Now, decimalValue);
        }
    }
}