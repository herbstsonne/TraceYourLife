using SQLite;
using System;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities;

namespace TraceYourLife.Database
{
    public class WeightPerDayRepository
    {
        private readonly SQLiteConnection conn;

        public WeightPerDayRepository(SQLiteConnection conn)
        {
            this.conn = conn;
        }

        public void InsertSetting(WeightPerDay diagramWeightChangeValueObject)
        {
            conn.Insert(diagramWeightChangeValueObject);
        }

        public WeightPerDay GetSetting(int personId)
        {
            WeightPerDay diagramWeightChangeValueObject = null;
            try
            {
                var tableSetting = conn.Table<WeightPerDay>();
                foreach (var entrySetting in tableSetting)
                {
                    if (entrySetting.PersonId.Equals(personId))
                        return new WeightPerDay()
                        {
                            Day = entrySetting.Day,
                            Weight = entrySetting.Weight,
                            PersonId = entrySetting.PersonId
                        };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return diagramWeightChangeValueObject;
        }
    }
}
