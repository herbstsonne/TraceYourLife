using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;

namespace TraceYourLife.Database
{
    public class TemperaturePerDayChartRepository
    {
        private SQLiteConnection connection;
        public TemperaturePerDayChartRepository(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        public List<TemperaturePerDay> Load28DaysCycle(IPerson person)
        {
            var tableCycle = connection.Table<TemperaturePerDay>();
            return tableCycle.Where(c => c.PersonId == person.Id).OrderByDescending(d => d.Date).Take(28).ToList();
        }

        public bool SaveNewData(TemperaturePerDay chart)
        {
            var tableCycle = connection.Table<TemperaturePerDay>();
            return connection.Insert(chart) >= 1;
        }

        public decimal? LoadValueOYesterday(IPerson person)
        {
            var tableCycle = connection.Table<TemperaturePerDay>();
            var cycleEntriesOfPerson = tableCycle.Where(c => c.PersonId == person.Id).ToList();
            var lastEntry = cycleEntriesOfPerson.Where(c => c.Date.Day == DateTime.Now.AddDays(-1).Day).FirstOrDefault();
            if(lastEntry != null)
                return lastEntry.BasalTemperature;
            return null;
        }

        public int CountEntriesOfDate(IPerson person, DateTime date)
        {
            var tableCycle = connection.Table<TemperaturePerDay>();
            var cycleEntriesOfPerson = tableCycle.Where(c => c.PersonId == person.Id).ToList();
            return cycleEntriesOfPerson.Where(c => c.Date.ToShortDateString() == date.ToShortDateString()).Count();
        }

        public void UpdateEntry(TemperaturePerDay entry)
        {
            connection.Update(entry);
        }
    }
}
