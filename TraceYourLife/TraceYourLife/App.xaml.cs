using System.Globalization;
using Xamarin.Forms;
using TraceYourLife.GUI.Views;
using TraceYourLife.Domain.Entities.Interfaces;

namespace TraceYourLife
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public static IPerson CurrentUser { get; set; }

        public App()
        {
            InitializeComponent();

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de-DE");
            if (!IsUserLoggedIn)
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new MainPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
