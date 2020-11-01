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
        private readonly TemperaturePerDayChartRepository _dbRepo;
        private readonly CycleData _cycleData;

        public TemperaturePerDayChartManager(CycleData cycleData)
        {
            _dbRepo = new TemperaturePerDayChartRepository();
            _cycleData = cycleData;
        }

        public List<TemperaturePerDay> GetBasalTempData()
        {
            return _dbRepo.Load28DaysCycle(App.CurrentUser, _cycleData.Id);
        }

        public CoverlineData? GetCoverlineData()
        {
            var coverlineCalculator = new CoverlineCalculator(_dbRepo.Load28DaysCycle(App.CurrentUser, _cycleData.Id));

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
                PersonId = App.CurrentUser.Id,
                CycleId = _cycleData.Id
            };

            return _dbRepo.SaveNewData(cycleEntry);
        }


        public void UpdateCycleEntryTable(DateTime date, decimal bTemp)
        {
            var entry = _dbRepo.GetEntry(date, App.CurrentUser.Id);
            if (entry == null)
            {
                entry = new TemperaturePerDay()
                {
                    Date = date.Date,
                    BasalTemperature = bTemp,
                    PersonId = App.CurrentUser.Id,
                    CycleId = _cycleData.Id
                };
                _dbRepo.SaveNewData(entry);
            }
            else
            {
                entry.BasalTemperature = bTemp;
                _dbRepo.UpdateEntry(entry);
            }
        }
    }
}
