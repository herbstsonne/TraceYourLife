using System;
using System.Threading.Tasks;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;
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
        private IPerson _person;
        private IPersonManager _manager;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _manager = new PersonManager();

            _person = await _manager.LoadFirstPerson() ?? new Person();
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();

            _businessSettings = new PersonDataHandler(_person);
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
            var gridName = new Grid();
            var gridAge = new Grid();
            var gridHeight = new Grid();
            var gridGender = new Grid();
            var gridStartWeight = new Grid();
            var gridPassword = new Grid();

            var frameName = GuiElementsFactory.CreateFrame();
            gridName.Children.Add(frameName);
            frameName.Content = editorName;
            var frameAge = GuiElementsFactory.CreateFrame();
            gridAge.Children.Add(frameAge);
            frameAge.Content = editorAge;
            var frameHeight = GuiElementsFactory.CreateFrame();
            gridHeight.Children.Add(frameHeight);
            frameHeight.Content = editorHeight;
            var frameGender = GuiElementsFactory.CreateFrame();
            gridGender.Children.Add(frameGender);
            frameGender.Content = pickerGender;
            var frameWeight = GuiElementsFactory.CreateFrame();
            gridStartWeight.Children.Add(frameWeight);
            frameWeight.Content = editorStartWeight;
            var framePassword = GuiElementsFactory.CreateFrame();
            gridPassword.Children.Add(framePassword);
            framePassword.Content = entryPassword;

            layout.Children.Add(gridName);
            layout.Children.Add(gridAge);
            layout.Children.Add(gridHeight);
            layout.Children.Add(gridGender);
            layout.Children.Add(gridStartWeight);
            layout.Children.Add(gridPassword);
            layout.Children.Add(buttonSave);
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            //if (!InputTypeCorrect())
            //    return;
            RenewValues();
            if (_manager.SavePerson(_person))
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
                _person.Age = age;
                _person.Height = height;
                _person.StartWeight = startWeight;
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
            _person.Name = editorName.Text;
            _person.Age = Convert.ToInt16(editorAge.Text);
            _person.Height = Convert.ToInt16(editorHeight.Text);
            _person.StartWeight = Convert.ToDecimal(editorStartWeight.Text);
            _person.Gender = (Gender?) pickerGender.SelectedItem ?? Gender.Female;
            _person.Password = entryPassword.Text;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public async Task ReloadPage()
        {
            _person = _person ?? await _manager.LoadFirstPerson() ?? new Person();
            SetPageParameters();
        }
    }
}