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
            return person?.Age.ToString();
        }

        public string GetPersonHeight()
        {
            return person?.Height.ToString();
        }

        public Gender GetPersonGender()
        {
            return person?.Gender ?? Gender.Female;
        }

        public string GetPersonStartweight()
        {
            return person?.StartWeight.ToString();
        }

        public string GetPersonPassword()
        {
            return person?.Password;
        }
    }
}
