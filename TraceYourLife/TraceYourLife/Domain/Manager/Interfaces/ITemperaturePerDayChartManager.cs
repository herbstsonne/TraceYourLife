using System;
using System.Collections.Generic;
using TraceYourLife.Domain.Entities;
using TraceYourLife.GUI.Views.Chart;

namespace TraceYourLife.Domain.Manager.Interfaces
{
    public interface ITemperaturePerDayChartManager
    {
        List<TemperaturePerDay> GetBasalTempData();
        CoverlineData? GetCoverlineData();
        bool SaveNewCycleEntry(DateTime date, decimal bTemp);
        void UpdateCycleEntryTable(DateTime date, decimal bTemp);
    }
}
