using SQLite;
using System;
using System.IO;
using TraceYourLife.Android;
using TraceYourLife.Database;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDatabaseConnection))]
namespace TraceYourLife.Android
{
    public class AndroidDatabaseConnection : DatabaseConnection
    {
        public override string SetConnectionString()
        {
            return Path.Combine(AppGlobal.DatabaseFilePathAndroid, AppGlobal.DatabaseFileName);
        }
    }
}