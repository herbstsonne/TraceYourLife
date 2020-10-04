using Rg.Plugins.Popup.Pages;
using System;
using TraceYourLife.Domain.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NextStepPopupPage : PopupPage
    {
        private IPerson person;

        public NextStepPopupPage(IPerson person, string header)
        {
            this.person = person;
            SetPageParameters(header);
        }

        private void SetPageParameters(string header)
        {
            InitializeComponent();
            BackgroundInputTransparent = true;
            HasKeyboardOffset = false;
            CloseWhenBackgroundIsClicked = true;

            var layout = GlobalGUISettings.InitializePopupLayout();
            this.Content = layout;

            Button buttonSetting = GlobalGUISettings.CreateButton("Einstellungen");
            buttonSetting.Clicked += ButtonSetting_Clicked;
            Button buttonDiagram = GlobalGUISettings.CreateButton("Diagramm");
            buttonDiagram.Clicked += ButtonDiagram_Clicked;

            layout.Children.Add(GlobalGUISettings.CreateLabel(header, 16));
            layout.Children.Add(buttonSetting);
            layout.Children.Add(buttonDiagram);
        }

        private void ButtonDiagram_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CycleChartPage(person));
        }

        private void ButtonSetting_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SettingsPage(person));
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}