using TraceYourLife.Database;
using Xamarin.Forms;
using TraceYourLife.Domain.Services;
using TraceYourLife.GUI.Views;

namespace TraceYourLife
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            MainPage = new MainPage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
