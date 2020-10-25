using System.Threading.Tasks;
using TraceYourLife.Database.Repositories;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Manager.Interfaces;

namespace TraceYourLife.Domain.Manager
{
    public class PersonManager : IPersonManager
    {
        private readonly PersonRepository _dbPerson;

        public PersonManager()
        {
            _dbPerson = new PersonRepository();
        }

        public async Task<IPerson> LoadFirstPerson()
        {
            return await _dbPerson.FirstFoundPerson();
        }

        public bool SavePerson(IPerson person)
        {
            return _dbPerson.UpdatePerson(person);
        }

        public async Task<IPerson> GetPerson(string name)
        {
            return await _dbPerson.SelectPerson(name);
        }
    }
}
