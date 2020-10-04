using System;
using SQLite;

namespace TraceYourLife.Domain.Entities
{
    public class WeightPerDay
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public double Weight { get; set; }
        public int PersonId { get; set; }
    }
}
