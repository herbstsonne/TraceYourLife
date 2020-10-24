using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraceYourLife.Domain.Entities;

namespace TraceYourLife.Database.Repositories
{
    public class WeightPerDayRepository
    {
        public void InsertSetting(WeightPerDay weightPerDay)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                lifeContext.WeightPerDay.Add(weightPerDay);
            }
        }

        public async Task<WeightPerDay> GetSetting(int personId)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                return await lifeContext.WeightPerDay.FirstOrDefaultAsync(w => w.PersonId == personId);
            }
        }
    }
}
