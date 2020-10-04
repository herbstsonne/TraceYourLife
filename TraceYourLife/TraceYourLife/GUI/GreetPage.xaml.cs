using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GreetPage : ContentPage
    {
        private IPerson person;
        public GreetPage(IPerson person = null)
        {
            this.person = person;
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
            var bgImage = new Image()
            {
                Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("miri1.jpg") : ImageSource.FromFile("miri1.jpg")
            };
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            var absLayout = new AbsoluteLayout()
            {
                Children = { { bgImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional },
                    { layout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional }
                }
            };
            this.Content = absLayout;

            var labelHeader = GlobalGUISettings.CreateLabel("Trace Your Life!", 35);
            var labelPoem = GlobalGUISettings.CreateLabel("It's the possibility of having a dream come true that makes life interesting.", 25);
            labelPoem.HorizontalOptions = LayoutOptions.CenterAndExpand;
            labelPoem.VerticalOptions = LayoutOptions.CenterAndExpand;

            Button buttonEnjoy = GlobalGUISettings.CreateButton("Entdecken!");
            buttonEnjoy.Clicked += ButtonEnjoy_Clicked;
            layout.Children.Add(labelHeader);
            layout.Children.Add(labelPoem);
            layout.Children.Add(buttonEnjoy);
            layout.Padding = new Thickness(5, 10);
        }

        private async void ButtonEnjoy_Clicked(object sender, EventArgs e)
        {
            //TODO merken, welche Person zuletzt eingeloggt war
            person = new Person().LoadFirstPerson();
            if (person != null)
            {
                await Navigation.PushPopupAsync(new LoginPage(person));
            }
            else
            {
                var createAccountPage = new AccountPage();
                await Navigation.PushModalAsync(createAccountPage);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}