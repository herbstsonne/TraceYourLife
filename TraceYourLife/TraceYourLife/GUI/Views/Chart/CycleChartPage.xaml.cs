using System;
using System.Threading.Tasks;
using OxyPlot.Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
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
        private IPerson person;
        private PersonManager _personManager;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _personManager = new PersonManager();
            person = await _personManager.LoadFirstPerson();
            if (person == null)
            {
                await Navigation.PushAsync(new SettingsPage());
                return;
            }
            SetPageParameters();
        }

        public async Task ReloadPage()
        {
            person = await _personManager.LoadFirstPerson();
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
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

            var cycleHandler = new TemperaturePerDayChartManager(person);
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
            Navigation.PushModalAsync(new SettingsPage());
            return true;
        }
    }
}