using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities;

namespace TraceYourLife
{
    public static class AppGlobal
    {
        public static string DatabaseFileName = "TYL.db";
        public static string DatabaseFilePathAndroid = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static string DatabaseFilePathiOS = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "..", "Library");
        public static SQLiteConnection DbConn;

        public static void CreateTables()
        {
            DbConn.CreateTables<Person, WeightPerDay, TemperaturePerDay>();
            //DbConn.DeleteAll<Person>();
        }
    }
}
