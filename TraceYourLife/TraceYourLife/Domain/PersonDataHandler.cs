using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Enums;

namespace TraceYourLife.Domain
{
    public class PersonDataHandler
    {
        private readonly IPerson person;
        public PersonDataHandler(IPerson person)
        {
            this.person = person;
        }

        public string GetPersonName()
        {
            return person?.Name;
        }

        public string GetPersonAge()
        {
            if (person?.Age == 0)
                return null;
            return person?.Age.ToString();
        }

        public string GetPersonHeight()
        {
            if (person?.Height == 0)
                return null;
            return person?.Height.ToString();
        }

        public Gender GetPersonGender()
        {
            return person?.Gender ?? Gender.Female;
        }

        public string GetPersonStartweight()
        {
            if (person?.StartWeight == 0)
                return null;
            return person?.StartWeight.ToString();
        }

        public string GetPersonPassword()
        {
            return person?.Password;
        }
    }
}
