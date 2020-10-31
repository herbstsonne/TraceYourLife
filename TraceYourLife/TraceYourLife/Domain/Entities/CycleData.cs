using System;
namespace TraceYourLife.Domain.Entities
{
    public class CycleData
    {
        public int Id { get; set; }

        public DateTime? FirstDayOfPeriod { get; set; }

        public DateTime? LastEnteredDay { get; set; }

        public DateTime? LastDay { get; set; }

        public int PersonId { get; set; }
    }
}
