using System;
using SQLite;

namespace TraceYourLife.Database
{
    public abstract class DatabaseConnection
    {
        public SQLiteConnection CreateDatabaseConnection()
        {
            try
            {
                var connectionString = SetConnectionString();
                return new SQLiteConnection(connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void CloseConnection(SQLiteConnection connection)
        {
            try
            {
                connection.Close();
                connection.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public abstract string SetConnectionString();
    }
}
