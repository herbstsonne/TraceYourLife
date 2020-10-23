using System;
using Rg.Plugins.Popup.Pages;
using TraceYourLife.Domain;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.GUI.Views.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : PopupPage, IInitializePage
    {
        private Entry editorName;
        private Entry entryPassword;
        private Label labelInformSuccessful;
        private IPerson person;
        private HandleBusinessSettings businessSettings;

        public LoginPage()
        {
            person = new Person().LoadFirstPerson();
            if (person == null)
                return;
            businessSettings = new HandleBusinessSettings(person);
            SetPageParameters();
        }

        public void ReloadPage()
        {
            person = new Person().LoadFirstPerson();
            businessSettings = new HandleBusinessSettings(person);
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();
            BackgroundInputTransparent = true;
            HasKeyboardOffset = false;
            CloseWhenBackgroundIsClicked = true;

            var layout = GlobalGUISettings.InitializePopupLayout();
            this.Content = layout;

            Button buttonDone = GlobalGUISettings.CreateButton("Login!");
            buttonDone.Clicked += ButtonDone_Clicked;

            editorName = GlobalGUISettings.CreateEntry(businessSettings.SetEditorNameText());
            editorName.Completed += EditorName_Completed;

            entryPassword = GlobalGUISettings.CreatePasswordField("");
            labelInformSuccessful = GlobalGUISettings.CreateLabel("", 10);

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

            if (person != null)
            {
                if (!person.Password.Equals(entryPassword.Text))
                {
                    labelInformSuccessful.Text = "Name in der Datenbank schon vorhanden. \n Anlegen nicht zweimal möglich ;)";
                }
                else
                {
                    ShowCycleChart();
                }
            }
            else
            {
                labelInformSuccessful.Text = "Ups...da ist was schiefgelaufen";
            }
        }

        private void ShowCycleChart()
        {
            Navigation.PushModalAsync(new CycleChartPage());
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