using System;
using System.Collections.Generic;
using Moq;
using TraceYourLife.Database.Repositories;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Manager;
using TraceYourLife.Domain.Manager.Interfaces;
using Xunit;

namespace UnitTests.Manager
{
    public class TemperaturePerDayChartManagerUnitTest
    {
        private ITemperaturePerDayChartManager _chartManager;

        public TemperaturePerDayChartManagerUnitTest()
        {
            var currentCycle = new CycleData { Id = 1, FirstDayOfPeriod = new DateTime(2020, 11, 1), PersonId = 1 };
            _chartManager = new TemperaturePerDayChartManager(currentCycle);
        }

        [Fact]
        public void Test1()
        {
            var mockRepo = new Mock<TemperaturePerDayChartRepository>();
            mockRepo.Setup(r => r.Load28DaysCycle(It.IsAny<IPerson>(), It.IsAny<int>()))
                .Returns(
                    new List<TemperaturePerDay>
                    {
                        new TemperaturePerDay { Id = 0, Date = new DateTime(2020, 11, 1), PersonId = 1, CycleId = 1, BasalTemperature = 36.5m},
                        new TemperaturePerDay { Id = 0, Date = new DateTime(2020, 11, 2), PersonId = 1, CycleId = 1, BasalTemperature = 36.5m},
                        new TemperaturePerDay { Id = 0, Date = new DateTime(2020, 11, 3), PersonId = 1, CycleId = 1, BasalTemperature = 36.5m},
                        new TemperaturePerDay { Id = 0, Date = new DateTime(2020, 11, 4), PersonId = 1, CycleId = 1, BasalTemperature = 36.5m},
                        new TemperaturePerDay { Id = 0, Date = new DateTime(2020, 11, 5), PersonId = 1, CycleId = 1, BasalTemperature = 36.5m},
                        new TemperaturePerDay { Id = 0, Date = new DateTime(2020, 11, 6), PersonId = 1, CycleId = 1, BasalTemperature = 36.5m},
                        new TemperaturePerDay { Id = 0, Date = new DateTime(2020, 11, 7), PersonId = 1, CycleId = 1, BasalTemperature = 36.5m},
                        new TemperaturePerDay { Id = 0, Date = new DateTime(2020, 11, 8), PersonId = 1, CycleId = 1, BasalTemperature = 36.5m},
                    }
                );
            var res = _chartManager.GetCoverlineData();
            Assert.Null(res);
        }
    }
}
