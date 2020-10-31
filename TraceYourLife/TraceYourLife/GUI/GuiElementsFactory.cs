using System;
using OxyPlot.Xamarin.Forms;
using System.Collections.Generic;
using TraceYourLife.Domain.Enums;
using TraceYourLife.GUI.Views.Chart;
using Xamarin.Forms;

namespace TraceYourLife.GUI
{
    public static class GuiElementsFactory
    {
        public static string GetFontFamily()
        {
            return Device.RuntimePlatform == Device.iOS ? "MarkerFelt-Thin" : "FFF_Tusj.ttf#FFF_Tusj";
        }

        public static Image CreateImage(string name)
        {
            return new Image()
            {
                Source = name,
                Aspect = Aspect.Fill
            };
        }

        public static Label CreateLabel(string text, int fontSize)
        {
            return new Label
            {
                Text = text,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.FromHex("#5DADE2"),
                FontSize = fontSize,
                FontFamily = GetFontFamily()
            };
        }

        public static Label CreateEditorLabel(string text)
        {
            return new Label
            {
                Text = text,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.FromHex("#5DADE2"),
                FontSize = 20,
                FontFamily = GetFontFamily()
            };
        }

        public static Entry CreatePasswordField(string placeholder, string text)
        {
            return new Entry
            {
                Placeholder = placeholder,
                Text = text,
                IsPassword = true,
                FontSize = 15,
                FontFamily = GetFontFamily(),
                Keyboard = Keyboard.Plain
            };
        }

        public static Entry CreateEntry(string placeholder, string text)
        {
            return new Entry
            {
                Text = text,
                Placeholder = placeholder,
                FontSize = 15,
                WidthRequest = 25,
                FontFamily = GetFontFamily(),
                Keyboard = Keyboard.Plain
            };
        }

        public static Editor CreateEditor(string editorText)
        {
            return new Editor
            {
                Text = editorText,
                WidthRequest = 25,
                FontSize = 15,
                FontFamily = GetFontFamily(),
                IsReadOnly = true,
                HeightRequest = 200
            };
        }

        public static Picker CreatePickerGender(Gender setGender)
        {
            return new Picker
            {
                ItemsSource = new List<Gender> { Gender.Female, Gender.Male },
                FontFamily = GetFontFamily(),
                FontSize = 15,
                WidthRequest = 25
            };
        }

        public static DatePicker CreateDatePicker()
        {
            return new DatePicker
            {
                FontFamily = GetFontFamily(),
                Format = "dd-MM-yy",
                BackgroundColor = Color.FromHex("#5DADE2"),
                Date = DateTime.Now
            };
        }

        public static PlotView CreatePlotModelCycle(ChartDrawer cycleHandler)
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

        public static Button CreateButtonInfo(string text)
        {
            return new Button
            {
                Text = text,
                BorderColor = Color.Black,
                BackgroundColor = Color.White,
                WidthRequest = 35,
                HeightRequest = 35,
                CornerRadius = 5,
                BorderWidth = 2,
                FontFamily = GetFontFamily()
            };
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
                CornerRadius = 5, 
                HeightRequest = 35,
                WidthRequest = 100,
                BackgroundColor = Color.White,
                FontFamily = GetFontFamily()
            };
        }

        public static StackLayout InitializePopupLayout()
        {
            var layout = new StackLayout
            {
                Padding = new Thickness(5, 10),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Opacity = 100,
                BackgroundColor = Color.FromHex("#76D7C4")
            };

            return layout;
        }
    }
}
