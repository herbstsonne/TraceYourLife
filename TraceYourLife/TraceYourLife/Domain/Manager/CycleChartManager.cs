using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using TraceYourLife.Domain.Manager.Interfaces;
using TraceYourLife.GUI;

namespace TraceYourLife.Domain.Manager
{
    public class CycleChartManager : ICycleChartManager
    {
        private readonly ITemperaturePerDayChartManager _temperaturePerDayChartManager;

        private LineSeries lsCycle;
        private IEnumerable<DataPoint> cyclePointList;
        public PlotModel LineChart { get; set; }

        public CycleChartManager(ITemperaturePerDayChartManager temperaturePerDayChartManager)
        {
            _temperaturePerDayChartManager = temperaturePerDayChartManager;
        }

        public void CreateLineChart(string title)
        {
            DefineLineChart(title);

            FillCyclePointList();
            CreateLineSeriesCycle();
            DefineAxes();

            LineChart.Series.Add(lsCycle);
        }

        public void FillCyclePointList()
        {
            var cycleData = _temperaturePerDayChartManager.RetrieveCycleOf();
            cyclePointList = cycleData.Select(source => new DataPoint(DateTimeAxis.ToDouble(source.Date), Convert.ToDouble(source.BasalTemperature))).Cast<DataPoint>().ToList();
        }

        private void DefineLineChart(string title)
        {
            LineChart = new PlotModel
            {
                Title = title,
                DefaultFont = GuiElementsFactory.UseFontFamilyFFFTusj(),
                LegendFont = GuiElementsFactory.UseFontFamilyFFFTusj(),
                TitleFont = GuiElementsFactory.UseFontFamilyFFFTusj(),
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
            Axis yTemp = new LinearAxis { Key = "BTempAxis", Position = AxisPosition.Left, Selectable = false, Title = "Basaltemperatur", MinimumMinorStep = 0.01, StartPosition = 35.0, AbsoluteMaximum = 40.0 };
            LineChart.Axes.Add(xDate);
            //LineChart.Axes.Add(yTemp);
            lsCycle.XAxisKey = xDate.Key;
            //lsCycle.YAxisKey = yTemp.Key;
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
    }
}
