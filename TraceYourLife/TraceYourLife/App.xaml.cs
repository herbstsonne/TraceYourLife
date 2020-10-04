using Xamarin.Forms;
using TraceYourLife.GUI;
using SQLite;
using TraceYourLife.Database;
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
            AppGlobal.DbConn = DependencyService.Get<DatabaseConnection>().CreateDatabaseConnection();
            AppGlobal.CreateTables();
            MainPage = new GreetPage();
        }

        protected override void OnSleep()
        {
            DependencyService.Get<DatabaseConnection>().CloseConnection(AppGlobal.DbConn);
        }

        protected override void OnResume()
        {
            AppGlobal.DbConn = DependencyService.Get<DatabaseConnection>().CreateDatabaseConnection();
        }
    }
}
