using System;
using System.Collections.Generic;
using TraceYourLife.Domain.Entities;

namespace TraceYourLife.Domain.Manager.Interfaces
{
    public interface ITemperaturePerDayChartDataLoader
    {
        List<TemperaturePerDay> RetrieveCycleOf();
        bool SaveNewCycleEntry(DateTime date, decimal bTemp);
        void UpdateCycleEntry(DateTime date, decimal bTemp);
        bool DoesEntryOfDateExists(DateTime date);
        decimal? SearchValueOfYesterday();
    }
}
