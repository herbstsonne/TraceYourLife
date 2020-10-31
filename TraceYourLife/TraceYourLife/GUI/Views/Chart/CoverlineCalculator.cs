using System;
using System.Collections.Generic;
using System.Linq;
using TraceYourLife.Domain.Entities;

namespace TraceYourLife.GUI.Views.Chart
{
    public class CoverlineCalculator
    {
        private List<TemperaturePerDay> _basalTempDataPoints;
        public CoverlineCalculator(List<TemperaturePerDay> basalTempDataPoints)
        {
            _basalTempDataPoints = basalTempDataPoints.OrderBy(b => b.Date).ToList();
        }

        public TemperaturePerDay GetOvulationEntry()
        { 
            var numberOfEntries = _basalTempDataPoints.Count();
            if (numberOfEntries < 9)
                return null;

            for (int i = 5; i < _basalTempDataPoints.Count() - 3; ++i)
            {
                var possibleOvulationEntry = _basalTempDataPoints[i];
                var successorList = new List<TemperaturePerDay> { _basalTempDataPoints[i + 1], _basalTempDataPoints[i + 2], _basalTempDataPoints[i + 3] };
                if (IsPossibleOvulationEntryTheCorrectOne(successorList, possibleOvulationEntry))
                    return possibleOvulationEntry;
            }
            return null;
        }

        public decimal? FindMaxTempBeforeOvulationDay(TemperaturePerDay ovulationEntry)
        {
            var indexOfOvulationEntry = GetIndexOfOvulationEntry(ovulationEntry);

            if (indexOfOvulationEntry == -1 || !_basalTempDataPoints.Any())
                return null;

            decimal? maxValue = _basalTempDataPoints[0].BasalTemperature;
            for (int i = indexOfOvulationEntry - 5; i < indexOfOvulationEntry; ++i)
            {
                var entry = _basalTempDataPoints[i];
                if (entry.BasalTemperature > maxValue)
                {
                    maxValue = entry.BasalTemperature;
                }
            }
            return maxValue;
        }


        private bool IsPossibleOvulationEntryTheCorrectOne(List<TemperaturePerDay> successorList, TemperaturePerDay possibleOvulationEntry)
        {
            var numberOfSuccessors = 0;
            foreach (var successor in successorList)
            {
                var diff = successor.BasalTemperature - possibleOvulationEntry.BasalTemperature;
                if (diff >= 0.2m)
                {
                    numberOfSuccessors++;
                    if (numberOfSuccessors == 3)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private int GetIndexOfOvulationEntry(TemperaturePerDay ovulationEntry)
        {
            return _basalTempDataPoints.FindIndex(entry => ReferenceEquals(entry, ovulationEntry));
        }
    }
}
