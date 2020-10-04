using SQLite;

namespace TraceYourLife.Database
{
    public class FoodRepository
    {
        private SQLiteConnection conn;
        public FoodRepository(SQLiteConnection conn)
        {
            this.conn = conn;
        }

        public void ShowDataInList()
        {
        }

        public void SaveChanges()
        {
        }
    }
}
