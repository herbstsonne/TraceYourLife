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

        public SettingsPage()
        {
        }

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

            var labelHeader = GlobalGUISettings.CreateLabel("Einstellungen", 30);
            layout.Children.Add(labelHeader);

            editorName = GlobalGUISettings.CreateEntry(businessSettings.SetEditorNameText());
            editorAge = GlobalGUISettings.CreateEntry(businessSettings.SetEditorAgeText());
            editorHeight = GlobalGUISettings.CreateEntry(businessSettings.SetEditorHeightText());
            pickerGender = GlobalGUISettings.CreatePickerGender(businessSettings.SetPickerGender());
            editorStartWeight = GlobalGUISettings.CreateEntry(businessSettings.SetEditorStartWeightText());
            entryPassword = GlobalGUISettings.CreatePasswordField(businessSettings.SetEntryPasswordText());
            var gridName = new Grid();
            var gridAge = new Grid();
            var gridHeight = new Grid();
            var gridGender = new Grid();
            var gridStartWeight = new Grid();
            var gridPassword = new Grid();
            gridName.Children.Add(GlobalGUISettings.CreateEditorLabel("Spitzname"), 0, 0);
            gridName.Children.Add(editorName, 1, 0);
            gridAge.Children.Add(GlobalGUISettings.CreateEditorLabel("Alter"), 0, 0);
            gridAge.Children.Add(editorAge, 1, 0);
            gridHeight.Children.Add(GlobalGUISettings.CreateEditorLabel("Größe"), 0, 0);
            gridHeight.Children.Add(editorHeight, 1, 0);
            //TODO dropdown
            gridGender.Children.Add(GlobalGUISettings.CreateEditorLabel("Geschlecht"), 0, 0);
            gridGender.Children.Add(pickerGender, 1, 0);
            gridStartWeight.Children.Add(GlobalGUISettings.CreateEditorLabel("Anfangsgewicht"), 0, 0);
            gridStartWeight.Children.Add(editorStartWeight, 1, 0);
            gridPassword.Children.Add(GlobalGUISettings.CreateEditorLabel("Passwort"), 0, 0);
            gridPassword.Children.Add(entryPassword, 1, 0);

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
                //Navigation.PushPopupAsync(new SuccessfulSavedPopupPage());
                await DisplayAlert("", "Einstellungen gespeichert", "OK");
            }
            else
            {
                //error message
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
            person.Gender = pickerGender.SelectedItem == null ? Gender.Female : (Gender)pickerGender.SelectedItem;
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