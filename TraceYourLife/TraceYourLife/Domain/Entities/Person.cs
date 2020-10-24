using SQLite;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Enums;

namespace TraceYourLife.Domain.Entities
{
    public class Person : IPerson
    {
        [PrimaryKey, AutoIncrement, Unique] 
        public int Id { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string Name { get; set; }

        public int Height { get; set; }

        public decimal StartWeight { get; set; }

        public string Password { get; set; }
    }
}
