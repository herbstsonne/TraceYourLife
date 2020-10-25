using System;
using Rg.Plugins.Popup.Pages;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views.Chart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CycleChartDataPopupPage : PopupPage
    {
        private readonly IPerson _person;
        private HandleBusinessSettings businessSettings;
        private TemperaturePerDayChartManager _chartHandler;
        private Label labelHeader;
        private DatePicker editorDate;
        private Entry pickerTemp;
        private StackLayout layout;
        private decimal? valueOfYesterday;
        private readonly string _header;

        public CycleChartDataPopupPage(IPerson person, string header)
        {
            _person = person;
            _header = header;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _chartHandler = new TemperaturePerDayChartManager(_person);
            valueOfYesterday = await _chartHandler.SearchValueOfYesterday();
            SetPageParameters(_header);
        }

        private void SetPageParameters(string header)
        {
            InitializeComponent();
            businessSettings = new HandleBusinessSettings(_person);
            BackgroundInputTransparent = true;
            HasKeyboardOffset = false;
            CloseWhenBackgroundIsClicked = true;

            layout = GlobalGUISettings.InitializePopupLayout();
            this.Content = layout;

            labelHeader = GlobalGUISettings.CreateLabel(header, 30);
            layout.Children.Add(labelHeader);

            editorDate = GlobalGUISettings.CreateDatePicker(businessSettings.SetCurrentDateText());
            var gridDate = new Grid();
            gridDate.Children.Add(GlobalGUISettings.CreateEditorLabel("Datum"), 0, 0);
            gridDate.Children.Add(editorDate, 1, 0);

            pickerTemp = GlobalGUISettings.CreateEntry("Gib die Temperatur ein", "");
            var gridTemp = new Grid();
            gridTemp.Children.Add(GlobalGUISettings.CreateEditorLabel("Temperatur"), 0, 0);
            gridTemp.Children.Add(pickerTemp, 1, 0);

            Button buttonDone = GlobalGUISettings.CreateButton("Done!");
            buttonDone.Clicked += ButtonDone_Clicked;

            var errorMessage = GlobalGUISettings.CreateLabel(String.Empty, 15);

            layout.Children.Add(gridDate);
            layout.Children.Add(gridTemp);
            layout.Children.Add(buttonDone);
        }

        private void ButtonDone_Clicked(object sender, EventArgs e)
        {
            /*if (_chartHandler.DoesEntryOfDateExists(editorDate.Date))
            {
                _chartHandler.UpdateCycleEntry(editorDate.Date, Convert.ToDecimal(pickerTemp));
                Navigation.PushModalAsync(new CycleChartPage());
                return;
            }
            if(_chartHandler.SaveNewCycleEntry(editorDate.Date, Convert.ToDecimal(pickerTemp)))
                Navigation.PushModalAsync(new CycleChartPage());*/
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}