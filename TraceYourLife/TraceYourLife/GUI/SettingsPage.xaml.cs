using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Enums;
using TraceYourLife.Domain.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        Entry editorName;
        Entry editorAge;
        Entry editorHeight;
        Picker pickerGender;
        Entry editorStartWeight;
        Entry entryPassword;
        private HandleBusinessSettings businessSettings;
        private IPerson person;

        public SettingsPage(IPerson person)
        {
            if (PopupNavigation.Instance.PopupStack.Any())
                PopupNavigation.Instance.PopAsync();
            this.person = person;
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

        private void ButtonSave_Clicked(object sender, EventArgs e)
        {
            if (!InputTypeCorrect())
                return;
            RenewValues();
            if (person.SavePerson())
            {
                Navigation.PushModalAsync(new CycleChartPage(person));
            }
            else
            {
                //error message
            }
        }

        private bool InputTypeCorrect()
        {
            int age;
            var ageNumeric = int.TryParse(editorAge.Text, out age);

            int height;
            var heightNumeric = int.TryParse(editorHeight.Text, out height);

            decimal startWeight;
            var weightNumeric = decimal.TryParse(editorStartWeight.Text, out startWeight);
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
    }
}