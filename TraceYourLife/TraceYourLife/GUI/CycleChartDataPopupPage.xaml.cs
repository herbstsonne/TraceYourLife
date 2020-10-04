using Rg.Plugins.Popup.Pages;
using System;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CycleChartDataPopupPage : PopupPage
    {
        private readonly IPerson person;
        private HandleBusinessSettings businessSettings;
        private TemperaturePerDayChartManager chartHandler;
        private Label labelHeader;
        private DatePicker editorDate;
        private Picker pickerTemp;
        private StackLayout layout;
        private Label errorMessage;

        public CycleChartDataPopupPage(IPerson person, string header)
        {
            this.person = person;
            SetPageParameters(header);
        }

        private void SetPageParameters(string header)
        {
            InitializeComponent();
            businessSettings = new HandleBusinessSettings(person);
            chartHandler = new TemperaturePerDayChartManager(person);
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

            decimal? valueOfYesterday = chartHandler.SearchValueOfYesterday();
            pickerTemp = GlobalGUISettings.CreatePickerTemperature(valueOfYesterday);
            var gridTemp = new Grid();
            gridTemp.Children.Add(GlobalGUISettings.CreateEditorLabel("Temperatur"), 0, 0);
            gridTemp.Children.Add(pickerTemp, 1, 0);

            Button buttonDone = GlobalGUISettings.CreateButton("Done!");
            buttonDone.Clicked += ButtonDone_Clicked;

            errorMessage = GlobalGUISettings.CreateLabel(String.Empty, 15);

            layout.Children.Add(gridDate);
            layout.Children.Add(gridTemp);
            layout.Children.Add(buttonDone);
        }

        private void ButtonDone_Clicked(object sender, EventArgs e)
        {
            if (pickerTemp.SelectedItem == null)
                return;
            var chartHandler = new TemperaturePerDayChartManager(person);

            if (chartHandler.DoesEntryOfDateExists(editorDate.Date))
            {
                chartHandler.UpdateCycleEntry(editorDate.Date, Convert.ToDecimal(pickerTemp.SelectedItem));
                Navigation.PushModalAsync(new CycleChartPage(person));
                return;
            }
            if(chartHandler.SaveNewCycleEntry(editorDate.Date, Convert.ToDecimal(pickerTemp.SelectedItem)))
                Navigation.PushModalAsync(new CycleChartPage(person));
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}