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
        private IPerson _person;
        private IPersonManager _personManager;
        private ICycleChartManager _chartHandler;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _personManager = new PersonManager();
            _person = await _personManager.LoadFirstPerson();
            if (_person == null)
            {
                await Navigation.PushAsync(new NavigationPage(new PersonalDataPage()));
                return;
            }
            _chartHandler = new CycleChartManager(new TemperaturePerDayChartManager(_person));
            SetPageParameters();
        }

        public async Task ReloadPage()
        {
            _person = _person ?? await _personManager.LoadFirstPerson();
            _chartHandler.FillCyclePointList();
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

            _chartHandler.CreateLineChart("Zyklus");
            PlotView view = GuiElementsFactory.CreatePlotModelCycle(_chartHandler);

            Button buttonInsertNewData = GuiElementsFactory.CreateButton("Neue Daten eingeben!");
            buttonInsertNewData.Clicked += ButtonInsertNewData_Clicked;

            layout.Children.Add(view);
            layout.Children.Add(buttonInsertNewData);
            layout.Padding = new Thickness(5, 10);
        }

        private async void ButtonInsertNewData_Clicked(object sender, EventArgs e)
        {
            var informationMessage = "Neue Daten eingeben";
            await Navigation.PushModalAsync(new CycleChartDataPopupPage(_person, informationMessage));
            await ReloadPage();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new NavigationPage(new PersonalDataPage()));
            return true;
        }
    }
}