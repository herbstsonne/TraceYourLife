using System;
using SQLite;

namespace TraceYourLife.Domain.Entities
{
    public class TemperaturePerDay
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal BasalTemperature { get; set; }

        public int PersonId { get; set; }
    }
}