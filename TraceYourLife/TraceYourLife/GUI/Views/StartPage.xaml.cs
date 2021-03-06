﻿using System.Threading.Tasks;
using TraceYourLife.GUI.Views.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage, IInitializePage
    {
        public StartPage()
        {
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
            var bgImage = GuiElementsFactory.CreateImage("buntpastellig.jpg");
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            var absLayout = new AbsoluteLayout()
            {
                Children = { { bgImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional },
                    { layout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional }
                }
            };
            this.Content = absLayout;

            var labelHeader = GuiElementsFactory.CreateLabel("Trace Your Life!", 35);
            var labelPoem = GuiElementsFactory.CreateLabel("It's the possibility of having a dream come true that makes life interesting.", 25);
            labelPoem.HorizontalOptions = LayoutOptions.CenterAndExpand;
            labelPoem.VerticalOptions = LayoutOptions.CenterAndExpand;

            layout.Children.Add(labelHeader);
            layout.Children.Add(labelPoem);
            layout.Padding = new Thickness(5, 10);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public void ReloadPage()
        {
        }
    }
}