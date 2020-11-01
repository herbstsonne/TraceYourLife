using System;

namespace TraceYourLife.Domain.Entities
{
    public class TemperaturePerDay
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal BasalTemperature { get; set; }

        public int PersonId { get; set; }

        public int CycleId { get; set; }
    }
}