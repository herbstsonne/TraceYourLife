
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
                "Die Basaltemperatur wird morgens vor dem Aufstehen gemessen " +
                "und entspricht in der ersten Zyklushälfte, " +
                "also vom Beginn der Monatsblutung bis zum Eisprung, " +
                "der normalen Körpertemperatur von etwa 36,5 Grad Celsius. " +
                "Nach dem Eisprung steigt sie um mindestens zwei Zehntel Grad Celsius an " +
                "und bleibt bis zur nächsten Monatsblutung so hoch." +
                "" +
                "Damit die Coverline, also eine Linie auf Höhe des höchsten Wertes der ersten sechs Messwerte, " +
                "angezeigt wird muss das Programm einen Eisprung ermittelt haben " +
                "und es müssen mindestens sechs Werte eingegeben worden sein." );

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
