using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessfulSavedPopupPage : PopupPage
    {
        private StackLayout layout;

        public SuccessfulSavedPopupPage()
        {
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
            BackgroundInputTransparent = true;
            HasKeyboardOffset = false;
            CloseWhenBackgroundIsClicked = true;

            layout = GlobalGUISettings.InitializePopupLayout();
            this.Content = layout;

            var labelHeader = GlobalGUISettings.CreateLabel("Speichern erfolgreich", 30);
            layout.Children.Add(labelHeader);
        }


        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}