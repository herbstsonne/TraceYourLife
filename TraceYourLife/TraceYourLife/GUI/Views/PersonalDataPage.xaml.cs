using System;
using System.Threading.Tasks;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Enums;
using TraceYourLife.Domain.Manager;
using TraceYourLife.Domain.Manager.Interfaces;
using TraceYourLife.GUI.Views.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalDataPage : ContentPage, IInitializePage
    {
        Entry editorName;
        Entry editorAge;
        Entry editorHeight;
        Picker pickerGender;
        Entry editorStartWeight;
        Entry entryPassword;
        private PersonDataHandler _businessSettings;
        private IPersonManager _manager;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _manager = new PersonManager();
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();

            _businessSettings = new PersonDataHandler(App.CurrentUser);
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            this.Content = layout;

            Button buttonSave = GuiElementsFactory.CreateButton("Speichern!");
            buttonSave.Clicked += ButtonSave_Clicked;

            var labelHeader = GuiElementsFactory.CreateLabel("Persönliche Daten", 30);
            layout.Children.Add(labelHeader);

            editorName = GuiElementsFactory.CreateEntry("Gib deinen Namen ein (Pflichtfeld)", _businessSettings.GetPersonName());
            editorAge = GuiElementsFactory.CreateEntry("Gib dein Alter ein", _businessSettings.GetPersonAge());
            editorHeight = GuiElementsFactory.CreateEntry("Gib deine Größe ein", _businessSettings.GetPersonHeight());
            pickerGender = GuiElementsFactory.CreatePickerGender(_businessSettings.GetPersonGender());
            editorStartWeight = GuiElementsFactory.CreateEntry("Gib dein Gewicht ein", _businessSettings.GetPersonStartweight());
            entryPassword = GuiElementsFactory.CreatePasswordField("Gib ein Passwort ein", _businessSettings.GetPersonPassword());

            layout.Children.Add(editorName);
            layout.Children.Add(editorAge);
            layout.Children.Add(editorHeight);
            layout.Children.Add(pickerGender);
            layout.Children.Add(editorStartWeight);
            layout.Children.Add(entryPassword);
            layout.Children.Add(buttonSave);
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            //if (!InputTypeCorrect())
            //    return;
            RenewValues();
            if (_manager.SavePerson(App.CurrentUser))
            {
                await DisplayAlert("", "Persönliche Daten gespeichert", "OK");
            }
            else
            {
                await DisplayAlert("", "Persönliche Daten konnten nicht gespeichert werden", "OK");
            }
        }

        private bool InputTypeCorrect()
        {
            var ageNumeric = int.TryParse(editorAge.Text, out var age);

            var heightNumeric = int.TryParse(editorHeight.Text, out var height);

            var weightNumeric = decimal.TryParse(editorStartWeight.Text, out var startWeight);
            if (ageNumeric && heightNumeric && weightNumeric)
            {

                App.CurrentUser.Age = age;
                App.CurrentUser.Height = height;
                App.CurrentUser.StartWeight = startWeight;
            }
            else
            {
                if (!ageNumeric)
                {
                    //error message
                }
                if (!heightNumeric)
                {
                    //error message
                }
                if (!weightNumeric)
                { }
            }
            return ageNumeric && heightNumeric && weightNumeric;
        }

        private void RenewValues()
        {
            App.CurrentUser.Name = editorName.Text;
            App.CurrentUser.Age = Convert.ToInt16(editorAge.Text);
            App.CurrentUser.Height = Convert.ToInt16(editorHeight.Text);
            App.CurrentUser.StartWeight = Convert.ToDecimal(editorStartWeight.Text);
            App.CurrentUser.Gender = (Gender?) pickerGender.SelectedItem ?? Gender.Female;
            App.CurrentUser.Password = entryPassword.Text;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public void ReloadPage()
        {
            SetPageParameters();
        }
    }
}