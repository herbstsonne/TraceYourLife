using System;

namespace TraceYourLife.Domain.Entities.Interfaces
{
    public interface IFitness
    {
        int Sport { get; set; }
        int Person { get; set; }
        double Duration { get; set; }
        DateTime Date { get; set; }
    }
}
