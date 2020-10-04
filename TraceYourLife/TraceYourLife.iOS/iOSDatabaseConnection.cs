using System;
using System.IO;
using TraceYourLife.Database;
using TraceYourLife.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(iOSDatabaseConnection))]
namespace TraceYourLife.iOS
{
    public class iOSDatabaseConnection : DatabaseConnection
    {
        public override string SetConnectionString()
        {
            return Path.Combine(AppGlobal.DatabaseFilePathiOS, AppGlobal.DatabaseFileName);
        }
    }
}