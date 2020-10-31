using System;
using System.Linq;
using TraceYourLife.Domain.Entities;

namespace TraceYourLife.Database.Repositories
{
    public class CycleDataRepository
    {
        public CycleDataRepository()
        {
        }

        public CycleData CreateCycleData(CycleData cycleDataEntry)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                var data = lifeContext.CycleData.Add(cycleDataEntry);
                lifeContext.SaveChanges();
                return data.Entity;
            }
        }

        public void UpdateCycleData(CycleData cycleDataEntry)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                lifeContext.CycleData.Update(cycleDataEntry);
                lifeContext.SaveChanges();
            }
        }

        public CycleData GetCurrentCycle()
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                //TODO: Datum prüfen
                return lifeContext.CycleData.Where(c => c.PersonId == App.CurrentUser.Id).FirstOrDefault();
            }
        }
    }
}
