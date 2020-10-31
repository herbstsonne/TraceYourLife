using System;
using System.Globalization;
using Rg.Plugins.Popup.Pages;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views.Chart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CycleChartDataPopupPage : PopupPage
    {
        private readonly string _header;
        private CycleData _cycleData;
        private TemperaturePerDayChartManager _temperaturePerDayChartManager;
        private CycleDataManager _cycleDataManager;
        private Label labelHeader;
        private DatePicker datePicker;
        private Entry pickerTemp;
        private StackLayout layout;

        public CycleChartDataPopupPage(string header)
        {
            _header = header;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _cycleDataManager = new CycleDataManager();
            _cycleData = _cycleDataManager.GetCurrentCycle();
            _temperaturePerDayChartManager = new TemperaturePerDayChartManager(_cycleData);
            SetPageParameters(_header);
        }

        private void SetPageParameters(string header)
        {
            InitializeComponent();
            BackgroundInputTransparent = true;
            HasKeyboardOffset = false;
            CloseWhenBackgroundIsClicked = true;

            layout = GuiElementsFactory.InitializePopupLayout();
            this.Content = layout;

            labelHeader = GuiElementsFactory.CreateLabel(header, 30);
            layout.Children.Add(labelHeader);

            datePicker = GuiElementsFactory.CreateBasalTempDatePicker(_cycleData);
            var gridDate = new Grid();
            gridDate.Children.Add(GuiElementsFactory.CreateEditorLabel("Datum"), 0, 0);
            gridDate.Children.Add(datePicker, 1, 0);

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
            if (!String.IsNullOrEmpty(pickerTemp.Text))
            {
                var decimalValue = decimal.Parse(pickerTemp.Text, new NumberFormatInfo() { NumberDecimalSeparator = "," });
                _temperaturePerDayChartManager.UpdateCycleEntryTable(datePicker.Date, decimalValue);
                _cycleData.LastEnteredDay = datePicker.Date;
                _cycleDataManager.UpdateCurrentCyle(_cycleData);
            }
            Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}