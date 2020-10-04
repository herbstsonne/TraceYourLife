using System;

namespace TraceYourLife.Domain.Entities.Interfaces
{
    public interface INutrition
    {
        int Person { get; set; }
        DateTime Date { get; set; }
        double Amount { get; set; }
    }
}
