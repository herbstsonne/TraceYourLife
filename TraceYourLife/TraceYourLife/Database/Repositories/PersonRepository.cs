using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;

namespace TraceYourLife.Database.Repositories
{
    public class PersonRepository
    {
        public async Task<IPerson> FirstFoundPerson()
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                return await lifeContext.Persons.FirstOrDefaultAsync();
            }
        }

        public bool InsertPerson(IPerson person)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                lifeContext.Persons.Add((Person)person);
                return lifeContext.SaveChanges() >= 1;
            }
        }

        public bool UpdatePerson(IPerson person)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                lifeContext.Persons.Update((Person)person);
                return lifeContext.SaveChanges() >= 1;
            }
        }

        public async Task<IPerson> SelectPerson(string name)
        {
            using (var lifeContext = new TraceYourLifeContext())
            {
                return await lifeContext.Persons.FirstOrDefaultAsync(p => p.Name == name);
            }
        }
    }
}
