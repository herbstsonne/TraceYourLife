using System;
using System.Globalization;
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
        private readonly string _header;
        private PersonDataHandler _businessSettings;
        private TemperaturePerDayChartManager _temperaturePerDayChartManager;
        private Label labelHeader;
        private DatePicker editorDate;
        private Entry pickerTemp;
        private StackLayout layout;

        public CycleChartDataPopupPage(IPerson person, string header)
        {
            _person = person;
            _header = header;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _temperaturePerDayChartManager = new TemperaturePerDayChartManager(_person);
            SetPageParameters(_header);
        }

        private void SetPageParameters(string header)
        {
            InitializeComponent();
            _businessSettings = new PersonDataHandler(_person);
            BackgroundInputTransparent = true;
            HasKeyboardOffset = false;
            CloseWhenBackgroundIsClicked = true;

            layout = GuiElementsFactory.InitializePopupLayout();
            this.Content = layout;

            labelHeader = GuiElementsFactory.CreateLabel(header, 30);
            layout.Children.Add(labelHeader);

            editorDate = GuiElementsFactory.CreateDatePicker();
            var gridDate = new Grid();
            gridDate.Children.Add(GuiElementsFactory.CreateEditorLabel("Datum"), 0, 0);
            gridDate.Children.Add(editorDate, 1, 0);

            pickerTemp = GuiElementsFactory.CreateEntry("Gib die Temperatur ein", "");
            var gridTemp = new Grid();
            gridTemp.Children.Add(GuiElementsFactory.CreateEditorLabel("Temperatur"), 0, 0);
            gridTemp.Children.Add(pickerTemp, 1, 0);

            Button buttonDone = GuiElementsFactory.CreateButton("Done!");
            buttonDone.Clicked += ButtonDone_Clicked;

            layout.Children.Add(gridDate);
            layout.Children.Add(gridTemp);
            layout.Children.Add(buttonDone);
        }

        private void ButtonDone_Clicked(object sender, EventArgs e)
        {
            var decimalValue = decimal.Parse(pickerTemp.Text, new NumberFormatInfo() { NumberDecimalSeparator = "," });
            _temperaturePerDayChartManager.UpdateCycleEntryTable(editorDate.Date, decimalValue);
            Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}