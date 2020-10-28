using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Series;

namespace TraceYourLife.GUI.Views.Chart
{
    public static class ChartFactory
    {
        public static PlotModel CreatePlotModel(string title)
        {
            return new PlotModel
            {
                Title = title,
                DefaultFont = GuiElementsFactory.GetFontFamily(),
                LegendFont = GuiElementsFactory.GetFontFamily(),
                TitleFont = GuiElementsFactory.GetFontFamily(),
                PlotType = PlotType.XY,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };
        }

        public static LineSeries CreateLineSeriesCycle(IEnumerable<DataPoint> cyclePointList, string title, OxyColor rgbColor, string dataFieldY)
        {
            var lineSeries = new LineSeries
            {
                Title = title,
                StrokeThickness = 1,
                Smooth = true,
                DataFieldX = "Date",
                DataFieldY = dataFieldY,
                Color = rgbColor,
                MarkerType = MarkerType.Star,
                MarkerSize = 4,
                MarkerStroke = rgbColor,
                MarkerFill = rgbColor,
                MarkerStrokeThickness = 2
            };
            lineSeries.Points.AddRange(cyclePointList);
            return lineSeries;
        }
    }
}
