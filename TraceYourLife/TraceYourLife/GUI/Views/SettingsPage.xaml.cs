using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Enums;
using TraceYourLife.Domain.Manager;
using TraceYourLife.GUI.Views.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage, IInitializePage
    {
        Entry editorName;
        Entry editorAge;
        Entry editorHeight;
        Picker pickerGender;
        Entry editorStartWeight;
        Entry entryPassword;
        private HandleBusinessSettings businessSettings;
        private IPerson person;
        private PersonManager manager;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            manager = new PersonManager();

            person = await manager.LoadFirstPerson() ?? new Person();
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();

            businessSettings = new HandleBusinessSettings(person);
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            this.Content = layout;

            Button buttonSave = GlobalGUISettings.CreateButton("Speichern!");
            buttonSave.Clicked += ButtonSave_Clicked;

            var labelHeader = GlobalGUISettings.CreateLabel("Persönliche Daten", 30);
            layout.Children.Add(labelHeader);

            editorName = GlobalGUISettings.CreateEntry("Gib deinen Namen ein (Pflichtfeld)", businessSettings.SetEditorNameText());
            editorAge = GlobalGUISettings.CreateEntry("Gib dein Alter ein", businessSettings.SetEditorAgeText());
            editorHeight = GlobalGUISettings.CreateEntry("Gib deine Größe ein", businessSettings.SetEditorHeightText());
            pickerGender = GlobalGUISettings.CreatePickerGender(businessSettings.SetPickerGender());
            editorStartWeight = GlobalGUISettings.CreateEntry("Gib dein Gewicht ein", businessSettings.SetEditorStartWeightText());
            entryPassword = GlobalGUISettings.CreatePasswordField("Gib ein Passwort ein", businessSettings.SetEntryPasswordText());
            var gridName = new Grid();
            var gridAge = new Grid();
            var gridHeight = new Grid();
            var gridGender = new Grid();
            var gridStartWeight = new Grid();
            var gridPassword = new Grid();

            var frameName = GlobalGUISettings.CreateFrame();
            gridName.Children.Add(frameName);
            frameName.Content = editorName;
            var frameAge = GlobalGUISettings.CreateFrame();
            gridAge.Children.Add(frameAge);
            frameAge.Content = editorAge;
            var frameHeight = GlobalGUISettings.CreateFrame();
            gridHeight.Children.Add(frameHeight);
            frameHeight.Content = editorHeight;
            var frameGender = GlobalGUISettings.CreateFrame();
            gridGender.Children.Add(frameGender);
            frameGender.Content = pickerGender;
            var frameWeight = GlobalGUISettings.CreateFrame();
            gridStartWeight.Children.Add(frameWeight);
            frameWeight.Content = editorStartWeight;
            var framePassword = GlobalGUISettings.CreateFrame();
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
            if (manager.SavePerson(person))
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
                person.Age = age;
                person.Height = height;
                person.StartWeight = startWeight;
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
            person.Name = editorName.Text;
            person.Age = Convert.ToInt16(editorAge.Text);
            person.Height = Convert.ToInt16(editorHeight.Text);
            person.StartWeight = Convert.ToDecimal(editorStartWeight.Text);
            person.Gender = (Gender?) pickerGender.SelectedItem ?? Gender.Female;
            person.Password = entryPassword.Text;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public async Task ReloadPage()
        {
            person = person ?? await manager.LoadFirstPerson() ?? new Person();
            SetPageParameters();
        }
    }
}