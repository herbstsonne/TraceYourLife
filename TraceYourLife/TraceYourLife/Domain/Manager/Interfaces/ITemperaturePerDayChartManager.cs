using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraceYourLife.Domain.Entities;

namespace TraceYourLife.Domain.Manager.Interfaces
{
    public interface ITemperaturePerDayChartManager
    {
        List<TemperaturePerDay> RetrieveCycleOf();
        bool SaveNewCycleEntry(DateTime date, decimal bTemp);
        void UpdateCycleEntryTable(DateTime date, decimal bTemp);
    }
}
