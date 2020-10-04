using System;
using SQLite;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;

namespace TraceYourLife.Database
{
    public class PersonRepository
    {
        private SQLiteConnection connection;
        public PersonRepository(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        public IPerson FirstFoundPerson()
        {
            try
            {
                var tablePerson = connection.Table<Person>();
                return tablePerson.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public bool InsertPerson(IPerson person)
        {
            return connection.Insert(person) >= 1;
        }

        public bool UpdatePerson(IPerson person)
        {
            return connection.Update(person) >= 1;
        }

        public IPerson SelectPerson(string name)
        {
            try
            {
                var tablePerson = connection.Table<Person>();
                foreach (var entryPerson in tablePerson)
                {
                    if (entryPerson.Name.Equals(name))
                    {
                        return new Person(entryPerson.Id, entryPerson.Name, entryPerson.Age, entryPerson.Height, entryPerson.Gender, entryPerson.StartWeight, entryPerson.Password);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}
