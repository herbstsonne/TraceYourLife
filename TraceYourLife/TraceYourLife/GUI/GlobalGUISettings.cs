using OxyPlot.Xamarin.Forms;
using System.Collections.Generic;
using TraceYourLife.Domain.Enums;
using TraceYourLife.Domain.Manager;
using Xamarin.Forms;

namespace TraceYourLife.GUI
{
    public static class GlobalGUISettings
    {
        public static string UseFontFamilyFFFTusj()
        {
            return Device.RuntimePlatform == Device.iOS ? "FFF_Tusj" :
                               Device.RuntimePlatform == Device.Android ? "FFF_Tusj.ttf#FFF_Tusj" :
                               "Assets/Fonts/FFF_Tusj.ttf#FFF_Tusj";
        }
        public static Label CreateLabel(string text, int fontSize)
        {
            return new Label
            {
                Text = text,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.CornflowerBlue,
                FontSize = fontSize,
                FontFamily = GlobalGUISettings.UseFontFamilyFFFTusj()
            };
        }

        public static Label CreateEditorLabel(string text)
        {
            return new Label
            {
                Text = text,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.Black,
                FontSize = 10,
                FontFamily = GlobalGUISettings.UseFontFamilyFFFTusj()
            };
        }

        public static Entry CreatePasswordField(string text)
        {
            return new Entry
            {
                Placeholder = "password",
                Text = text,
                IsPassword = true,
                FontSize = 10
            };
        }

        public static Entry CreateEntry(string text)
        {
            return new Entry
            {
                Text = text,
                BackgroundColor = Color.FromHex("#64DAED"),
                FontSize = 10,
                WidthRequest = 25,
                FontFamily = UseFontFamilyFFFTusj()
            };
        }

        public static Editor CreateEditor(string editorText = null)
        {
            return new Editor
            {
                Text = editorText,
                BackgroundColor = Color.FromHex("#64DAED"),
                WidthRequest = 25,
                FontSize = 10
            };
        }

        public static Picker CreatePickerTemperature(decimal? valueOfYesterday)
        {
            var maxTemp = 38.5m;
            var curTemp = 35.5m;
            var allTemps = new List<decimal>();
            while (curTemp < maxTemp)
            {
                allTemps.Add(curTemp);
                curTemp += 0.01m;
            }
            
            return new Picker
            {
                ItemsSource = allTemps,
                SelectedItem = valueOfYesterday != null ? valueOfYesterday : null,
                FontFamily = UseFontFamilyFFFTusj(),
                FontSize = 10,
                WidthRequest = 25
            };
        }

        public static Picker CreatePickerGender(Gender setGender)
        {
            return new Picker
            {
                ItemsSource = new List<Gender> { Gender.Female, Gender.Male },
                FontFamily = UseFontFamilyFFFTusj(),
                FontSize = 10,
                WidthRequest = 25
            };
        }

        public static DatePicker CreateDatePicker(object globalGuiSettings)
        {
            return new DatePicker
            {
                FontFamily = UseFontFamilyFFFTusj(),
                Format = "dd-MM-yy",
                BackgroundColor = Color.FromHex("#64DAED")
            };
        }

        public static PlotView CreatePlotModelCycle(TemperaturePerDayChartManager cycleHandler)
        {
            PlotView view = new PlotView();
            view.SetBinding(PlotView.ModelProperty, new Binding("LineChart"));
            view.BindingContext = cycleHandler;
            view.WidthRequest = 400;
            view.HeightRequest = 300;
            view.VerticalOptions = LayoutOptions.FillAndExpand;
            view.HorizontalOptions = LayoutOptions.FillAndExpand;
            return view;
        }

        public static Button CreateButton(string text)
        {
            return new Button
            {
                Text = text,
                HorizontalOptions = LayoutOptions.Center,
                BorderColor = Color.Black,
                ImageSource = "icon.png",
                BorderWidth = 2,
                CornerRadius = 10, 
                HeightRequest = 35,
                BackgroundColor = Color.White,
                FontFamily = GlobalGUISettings.UseFontFamilyFFFTusj()
            };
        }

        public static StackLayout InitializePopupLayout()
        {
            var layout = new StackLayout
            {
                Padding = new Thickness(5, 10),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("#64DAED"),
                Opacity = 100
            };

            return layout;
        }
    }
}
