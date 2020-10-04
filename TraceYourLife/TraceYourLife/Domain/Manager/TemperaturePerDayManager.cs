using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using TraceYourLife.Database;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Interfaces;
using TraceYourLife.GUI;

namespace TraceYourLife.Domain.Manager
{
    public class TemperaturePerDayManager
    {
        private IPerson person;
        private LineSeries lsCycle;
        IEnumerable<DataPoint> cyclePointList;

        public PlotModel LineChart { get; set; }

        public TemperaturePerDayManager(IPerson person)
        {
            this.person = person;
        }

        #region Database
        public List<TemperaturePerDay> RetrieveCycleOf()
        {
            var dbCycle = new TemperatureDiagramRepository(AppGlobal.DbConn);
            return dbCycle.Load28DaysCycle(person);
        }

        public bool SaveNewCycleEntry(DateTime date, decimal bTemp)
        {
            var cycleEntry = new TemperaturePerDay()
            {
                Date = date,
                BasalTemperature = bTemp,
                PersonId = person.Id
            };

            var dbCycle = new TemperatureDiagramRepository(AppGlobal.DbConn);
            return dbCycle.SaveNewData(cycleEntry);
        }

        public void UpdateCycleEntry(DateTime date, decimal bTemp)
        {
            var cycleEntry = new TemperaturePerDay()
            {
                Date = date,
                BasalTemperature = bTemp,
                PersonId = person.Id
            };

            var dbCycle = new TemperatureDiagramRepository(AppGlobal.DbConn);
            dbCycle.UpdateEntry(cycleEntry);
        }

        public bool DoesEntryOfDateExists(DateTime date)
        {
            var dbCycle = new TemperatureDiagramRepository(AppGlobal.DbConn);
            var num = dbCycle.CountEntriesOfDate(person, date);
            return num > 0;
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

        public decimal? SearchValueOfYesterday()
        {
            var dbCycle = new TemperatureDiagramRepository(AppGlobal.DbConn);
            return dbCycle.LoadValueOYesterday(person);
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
                Color = OxyColors.CornflowerBlue,
                MarkerType = MarkerType.Star,
                MarkerSize = 4,
                MarkerStroke = OxyColors.CornflowerBlue,
                MarkerFill = OxyColors.CornflowerBlue,
                MarkerStrokeThickness = 1.5
            };
            lsCycle.Points.AddRange(cyclePointList);
        }
        #endregion
    }
}
