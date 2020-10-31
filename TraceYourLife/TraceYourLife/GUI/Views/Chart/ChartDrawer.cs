using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using TraceYourLife.Domain.Entities;

namespace TraceYourLife.GUI.Views.Chart
{
    public class ChartDrawer
    {
        public PlotModel LineChart { get; set; }

        public void CreateLineChart(string title, Func<List<TemperaturePerDay>> cycleData, Func<CoverlineData?> coverlineData)
        {
            LineChart = ChartFactory.CreatePlotModel(title);

            var tempPointList = FillBasalTempPointList(cycleData);
            var ovuTempList = FillOvulationLineList(coverlineData);
            DefineAxes();


            LineChart.Series.Add(ChartFactory.CreateLineSeries(tempPointList,
                "Temperaturverlauf", OxyColor.FromRgb(245, 176, 65), "BasalTemperature"));

            LineChart.Series.Add(ChartFactory.CreateLineSeries(ovuTempList,
                "Coverline", OxyColor.FromRgb(165, 105, 189), "Ovulationline"));
        }

        private void DefineAxes()
        {
            Axis xDate = new DateTimeAxis { Key = "DateAxis", Position = AxisPosition.Bottom, IntervalType = DateTimeIntervalType.Days, Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(-11)), Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(+19)), Selectable = false, Title = "Tag", StringFormat = "dd-MM-yy" };
            Axis yTemp = new LinearAxis { Key = "BTempAxis", Position = AxisPosition.Left, Selectable = false, Title = "Basaltemperatur", Minimum = 35.0, Maximum = 38.0, MinimumMinorStep = 0.01, AbsoluteMaximum = 40.0 };

            LineChart.Axes.Add(xDate);
            LineChart.Axes.Add(yTemp);
            //lsCycle.YAxisKey = yTemp.Key;
        }

        public IEnumerable<DataPoint> FillBasalTempPointList(Func<List<TemperaturePerDay>> cycleData)
        {
            var cyclePoints = cycleData();
            return cyclePoints.Select(source => new DataPoint(DateTimeAxis.ToDouble(source.Date), Convert.ToDouble(source.BasalTemperature))).Cast<DataPoint>().ToList();
        }

        public IEnumerable<DataPoint> FillOvulationLineList(Func<CoverlineData?> coverlineData)
        {
            var data = coverlineData();
            if (data == null)
                return new List<DataPoint>();
            var lineData = (CoverlineData)data;

            return new List<DataPoint>
            {
                new DataPoint(DateTimeAxis.ToDouble(lineData.DayBeforeOvulation.AddDays(-10)), Convert.ToDouble(lineData.LineValue)),
                new DataPoint(DateTimeAxis.ToDouble(lineData.DayBeforeOvulation.AddDays(18)), Convert.ToDouble(lineData.LineValue)),
            };
        }
    }
}
