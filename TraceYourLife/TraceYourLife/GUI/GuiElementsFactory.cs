using System;
using OxyPlot.Xamarin.Forms;
using System.Collections.Generic;
using TraceYourLife.Domain.Enums;
using TraceYourLife.Domain.Manager.Interfaces;
using Xamarin.Forms;

namespace TraceYourLife.GUI
{
    public static class GuiElementsFactory
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
                FontFamily = GuiElementsFactory.UseFontFamilyFFFTusj()
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
                FontSize = 20,
                FontFamily = UseFontFamilyFFFTusj()
            };
        }

        public static Entry CreatePasswordField(string placeholder, string text)
        {
            return new Entry
            {
                Placeholder = placeholder,
                Text = text,
                IsPassword = true,
                FontSize = 10
            };
        }

        public static Frame CreateFrame()
        {
            return new Frame()
            {
                BorderColor = Color.Black
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

        public static Picker CreatePickerGender(Gender setGender)
        {
            return new Picker
            {
                ItemsSource = new List<Gender> { Gender.Female, Gender.Male },
                FontFamily = UseFontFamilyFFFTusj(),
                FontSize = 15,
                WidthRequest = 25
            };
        }

        public static DatePicker CreateDatePicker()
        {
            return new DatePicker
            {
                FontFamily = UseFontFamilyFFFTusj(),
                Format = "dd-MM-yy",
                BackgroundColor = Color.FromHex("#64DAED"),
                Date = DateTime.Now
            };
        }

        public static PlotView CreatePlotModelCycle(ICycleChartManager cycleHandler)
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
                FontFamily = GuiElementsFactory.UseFontFamilyFFFTusj()
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
