using System;
using System.Collections.Generic;
using TraceYourLife.Database.Repositories;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Manager.Interfaces;
using TraceYourLife.GUI.Views.Chart;

namespace TraceYourLife.Domain.Manager
{
    public class TemperaturePerDayChartManager : ITemperaturePerDayChartManager
    {
        public TemperaturePerDay OvulationEntry { get; private set; }
        private readonly TemperaturePerDayChartRepository _dbCycle;

        public TemperaturePerDayChartManager()
        {
            _dbCycle = new TemperaturePerDayChartRepository();
        }

        public List<TemperaturePerDay> GetBasalTempData()
        {
            return _dbCycle.Load28DaysCycle(App.CurrentUser);
        }

        public CoverlineData? GetCoverlineData()
        {
            var coverlineCalculator = new CoverlineCalculator(_dbCycle.Load28DaysCycle(App.CurrentUser));

            OvulationEntry = coverlineCalculator.GetOvulationEntry();
            var maxValue = coverlineCalculator.FindMaxTempBeforeOvulationDay(OvulationEntry);

            if (OvulationEntry == null || maxValue == null)
                return null;
            return new CoverlineData { LineValue = (decimal)maxValue, DayBeforeOvulation = OvulationEntry.Date };
        }

        public bool SaveNewCycleEntry(DateTime date, decimal bTemp)
        {
            var cycleEntry = new TemperaturePerDay()
            {
                Date = date.Date,
                BasalTemperature = bTemp,
                PersonId = App.CurrentUser.Id
            };

            return _dbCycle.SaveNewData(cycleEntry);
        }


        public void UpdateCycleEntryTable(DateTime date, decimal bTemp)
        {
            var entry = _dbCycle.GetEntry(date, App.CurrentUser.Id);
            if (entry == null)
            {
                entry = new TemperaturePerDay()
                {
                    Date = date.Date,
                    BasalTemperature = bTemp,
                    PersonId = App.CurrentUser.Id
                };
                _dbCycle.SaveNewData(entry);
            }
            else
            {
                entry.BasalTemperature = bTemp;
                _dbCycle.UpdateEntry(entry);
            }
        }
    }
}
