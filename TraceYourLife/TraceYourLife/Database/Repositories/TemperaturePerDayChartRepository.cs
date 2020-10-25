using System;
using System.Collections.Generic;
using System.Linq;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;

namespace TraceYourLife.Database.Repositories
{
    public class TemperaturePerDayChartRepository
    {
        public List<TemperaturePerDay> Load28DaysCycle(IPerson person)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                return lifeContext.TemperaturePerDay.Where(c => c.PersonId == person.Id).OrderByDescending(d => d.Date.Date).Take(28).ToList();
            }
        }

        public bool SaveNewData(TemperaturePerDay chart)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                lifeContext.Add(chart);
                return lifeContext.SaveChanges() >= 1;
            }
        }

        public void UpdateEntry(TemperaturePerDay entry)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                lifeContext.TemperaturePerDay.Update(entry); 
                lifeContext.SaveChanges();
            }
        }

        public TemperaturePerDay GetEntry(DateTime date, int personId)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                var allEntriesOfCurrentPerson = lifeContext.TemperaturePerDay.Where(c => c.PersonId == personId).ToList();
                return allEntriesOfCurrentPerson.FirstOrDefault(t => t.Date.Date == date.Date);
            }
        }
    }
}
