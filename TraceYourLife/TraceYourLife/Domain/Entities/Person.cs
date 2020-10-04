using SQLite;
using TraceYourLife.Database;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Enums;

namespace TraceYourLife.Domain.Entities
{
    public class Person : IPerson
    {
        private int id;
        #region Properties
        [PrimaryKey, AutoIncrement, Unique]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string Name { get; set; }

        public int Height { get; set; }

        public decimal StartWeight { get; set; }

        public string Password { get; set; }
        #endregion
        #region Constructors
        public Person()
        { }

        public Person(int id, string name, int age, int height, Gender gender, decimal weight, string password)
        {
            this.id = id;
            Name = name;
            Age = age;
            Height = height;
            Gender = gender;
            StartWeight = weight;
            Password = password;
        }
        #endregion
        #region Public methods
        public IPerson LoadFirstPerson()
        {
            var dbPerson = new PersonRepository(AppGlobal.DbConn);
            return dbPerson.FirstFoundPerson();
        }

        public bool SavePerson()
        {
            var dbPerson = new PersonRepository(AppGlobal.DbConn);
            var person = GetPerson(Name);
            bool successful = false;
            if (person == null)
            {
                successful = dbPerson.InsertPerson(this);
            }
            else
            {
                successful = dbPerson.UpdatePerson(person);
            }
            return successful;
        }

        public IPerson GetPerson(string name)
        {
            return new PersonRepository(AppGlobal.DbConn).SelectPerson(name);
        }
        #endregion
    }
}
