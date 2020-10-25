using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using TraceYourLife.Database.Repositories;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Manager.Interfaces;
using TraceYourLife.GUI;

namespace TraceYourLife.Domain.Manager
{
    public class TemperaturePerDayChartManager : ITemperaturePerDayChartDataLoader, ITemperaturePerDayChartInitializer
    {
        private readonly IPerson person;
        private LineSeries lsCycle;
        private IEnumerable<DataPoint> cyclePointList;
        private readonly TemperaturePerDayChartRepository dbCycle;

        public PlotModel LineChart { get; set; }

        public TemperaturePerDayChartManager(IPerson person)
        {
            this.person = person;
            dbCycle = new TemperaturePerDayChartRepository();
        }

        #region Load data
        public List<TemperaturePerDay> RetrieveCycleOf()
        {
            return dbCycle.Load28DaysCycle(person);
        }

        public bool SaveNewCycleEntry(DateTime date, decimal bTemp)
        {
            var cycleEntry = new TemperaturePerDay()
            {
                Date = date.Date,
                BasalTemperature = bTemp,
                PersonId = person.Id
            };

            return dbCycle.SaveNewData(cycleEntry);
        }


        public void UpdateCycleEntryTable(DateTime date, decimal bTemp)
        {
            var entry = dbCycle.GetEntry(date, person.Id);
            if (entry == null)
            {
                entry = new TemperaturePerDay()
                {
                    Date = date.Date,
                    BasalTemperature = bTemp,
                    PersonId = person.Id
                };
                dbCycle.SaveNewData(entry);
            }
            else
            {
                entry.BasalTemperature = bTemp;
                dbCycle.UpdateEntry(entry);
            }
        }

        public bool DoesEntryOfDateExists(DateTime date)
        {
            var num = dbCycle.CountEntriesOfDate(person, date);
            return num > 0;
        }

        public async Task<decimal?> SearchValueOfYesterday()
        {
            return await dbCycle.LoadValueOYesterday(person);
        }
        #endregion
        #region Chart
        public void CreateLineChart(string title)
        {
            DefineLineChart(title);

            FillCyclePointList();
            CreateLineSeriesCycle();
            DefineAxes();

            LineChart.Series.Add(lsCycle);
        }

        private void DefineLineChart(string title)
        {
            LineChart = new PlotModel
            {
                Title = title,
                DefaultFont = GlobalGUISettings.UseFontFamilyFFFTusj(),
                LegendFont = GlobalGUISettings.UseFontFamilyFFFTusj(),
                TitleFont = GlobalGUISettings.UseFontFamilyFFFTusj(),
                PlotType = PlotType.XY,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };
        }

        private void DefineAxes()
        {
            Axis xDate = new DateTimeAxis { Key = "DateAxis", Position = AxisPosition.Bottom, IntervalType = DateTimeIntervalType.Days, Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(-10)), Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(+3)), Selectable = false, Title = "Tag", StringFormat = "dd-MM-yy" };
            Axis yTemp = new LinearAxis { Key = "BTempAxis", Position = AxisPosition.Left, Selectable = false, Title = "Basaltemperatur", MinimumMinorStep = 0.01, StartPosition = 35.0, AbsoluteMaximum = 40.0};
            LineChart.Axes.Add(xDate);
            //LineChart.Axes.Add(yTemp);
            lsCycle.XAxisKey = xDate.Key;
            //lsCycle.YAxisKey = yTemp.Key;
        }

        private void FillCyclePointList()
        {
            var cycleData = RetrieveCycleOf();
            cyclePointList = cycleData.Select(source => new DataPoint(DateTimeAxis.ToDouble(source.Date), Convert.ToDouble(source.BasalTemperature))).Cast<DataPoint>().ToList();
        }

        private void CreateLineSeriesCycle()
        {
            lsCycle = new LineSeries
            {
                Title = "Temperaturverlauf",
                StrokeThickness = 1,
                Smooth = true,
                DataFieldX = "Date",
                DataFieldY = "BasalTemperature",
                Color = OxyColors.Yellow,
                MarkerType = MarkerType.Star,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Yellow,
                MarkerFill = OxyColors.Yellow,
                MarkerStrokeThickness = 1.5
            };
            lsCycle.Points.AddRange(cyclePointList);
        }
        #endregion
    }
}
