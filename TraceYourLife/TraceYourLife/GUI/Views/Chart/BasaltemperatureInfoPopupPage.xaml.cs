
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views.Chart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasaltemperatureInfoPopupPage : PopupPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetPageParameters("Was kann mir meine Basaltemperatur verraten?");
        }

        private void SetPageParameters(string header)
        {
            InitializeComponent();
            BackgroundInputTransparent = true;
            HasKeyboardOffset = false;
            CloseWhenBackgroundIsClicked = true;

            var layout = GuiElementsFactory.InitializePopupLayout();
            this.Content = layout;

            var labelHeader = GuiElementsFactory.CreateLabel(header, 25);
            layout.Children.Add(labelHeader);

            var textfield = GuiElementsFactory.CreateEditor(
                "Die Aufwachtemperatur entspricht in der ersten Zyklushälfte, " +
                "also vom Beginn der Monatsblutung bis zum Eisprung, " +
                "der normalen Körpertemperatur von etwa 36,5 Grad Celsius. " +
                "Nach dem Eisprung steigt sie um mindestens zwei Zehntel Grad Celsius an " +
                "und bleibt bis zur nächsten Monatsblutung so hoch. Gemessen wird morgens direkt nach dem Aufwachen, " +
                "noch vor dem Aufstehen. Vor dem Messen sollte die Frau zumindest eine, " +
                "besser einige Stunden geschlafen haben.");

            var buttonClose = GuiElementsFactory.CreateButton("Schließen");
            buttonClose.Clicked += ButtonClose_Clicked;
            layout.Children.Add(textfield);
            layout.Children.Add(buttonClose);
        }

        private async void ButtonClose_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
