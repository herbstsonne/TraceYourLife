using System;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Enums;
using TraceYourLife.Domain.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private Label labelHeader;
        private Entry editorName;
        private Entry entryPassword;
        private Label labelInformSuccessful;
        private IPerson person;

        public AccountPage()
        {
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();

            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            this.Content = layout;

            Button buttonDone = GlobalGUISettings.CreateButton("Done!");
            buttonDone.Clicked += ButtonDone_Clicked;

            editorName = GlobalGUISettings.CreateEntry(String.Empty);
            editorName.Completed += EditorName_Completed;

            entryPassword = GlobalGUISettings.CreatePasswordField("");
            labelInformSuccessful = GlobalGUISettings.CreateLabel("", 10);

            labelHeader = GlobalGUISettings.CreateLabel("Account anlegen", 30);
            layout.Children.Add(labelHeader);

            var gridName = new Grid();
            var gridPassword = new Grid();
            gridName.Children.Add(GlobalGUISettings.CreateEditorLabel("Spitzname"), 0, 0);
            gridName.Children.Add(editorName, 1, 0);
            gridPassword.Children.Add(GlobalGUISettings.CreateEditorLabel("Passwort"), 0, 0);
            gridPassword.Children.Add(entryPassword, 1, 0);

            layout.Children.Add(gridName);
            layout.Children.Add(gridPassword);
            layout.Children.Add(buttonDone);
            layout.Children.Add(labelInformSuccessful);
        }

        private void EditorName_Completed(object sender, EventArgs e)
        {
            var editor = sender as Editor;
            person = new Person().GetPerson(editor.Text);
        }

        private void ButtonDone_Clicked(object sender, EventArgs e)
        {
            labelInformSuccessful.Text = "";
            if (!EditorTextSet())
                return;

            var newPerson = new Person(-1, editorName.Text, 0, 0, Gender.Female, 0, entryPassword.Text);

            if (newPerson.SavePerson())
            {
                person = newPerson.GetPerson(newPerson.Name);
                ShowSettingsPage();
            }
            else
            {
                labelInformSuccessful.Text = "Ups...da ist was schiefgelaufen";
            }
        }

        private void ShowSettingsPage()
        {
            Navigation.PushModalAsync(new SettingsPage(person));
        }

        private bool EditorTextSet()
        {
            if (String.IsNullOrEmpty(editorName.Text))
            {
                labelInformSuccessful.Text = "Bitte Name eingeben!";
                return false;
            }
            return true;

        }
    }
}