using System;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Manager;
using TraceYourLife.Domain.Manager.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TraceYourLife.GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private Entry editorName;
        private Entry entryPassword;
        private Label labelInformSuccessful;
        private IPerson person;
        private IPersonManager manager;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.IsUserLoggedIn = false;
            App.CurrentUser = null;
            manager = new PersonManager();
            SetPageParameters();
        }

        private void SetPageParameters()
        {
            InitializeComponent();

            var layout = GuiElementsFactory.InitializePopupLayout();
            this.Content = layout;

            Button buttonSignUp = GuiElementsFactory.CreateButton("Registrieren");
            buttonSignUp.Clicked += ButtonSignUp_Clicked;

            Button buttonDone = GuiElementsFactory.CreateButton("Login");
            buttonDone.Clicked += ButtonDone_Clicked;

            editorName = GuiElementsFactory.CreateEntry("Gib deinen Namen ein", null);

            entryPassword = GuiElementsFactory.CreatePasswordField("Gib ein Passwort ein", "");
            labelInformSuccessful = GuiElementsFactory.CreateLabel("", 20);

            var gridButtons = new Grid();
            gridButtons.Children.Add(buttonSignUp, 0, 0);
            gridButtons.Children.Add(buttonDone, 2, 0);

            layout.Children.Add(editorName);
            layout.Children.Add(entryPassword);
            layout.Children.Add(gridButtons);
            layout.Children.Add(labelInformSuccessful);
        }

        private void ButtonSignUp_Clicked(object sender, EventArgs e)
        {
            person = new Person
            {
                Name = editorName.Text,
                Password = entryPassword.Text
            };
            var saved = manager.SavePerson(person);

            labelInformSuccessful.Text = "";
            if (saved)
            {
                PrepareMainPage("Registrierung erfolgreich");
            }
            else
            {
                labelInformSuccessful.Text = "Registrierung nicht erfolgreich";
            }
        }

        private async void ButtonDone_Clicked(object sender, EventArgs e)
        {
            labelInformSuccessful.Text = "";

            person = await manager.GetPerson(editorName.Text);
            if (person != null && person.Password.Equals(entryPassword.Text))
            {
                PrepareMainPage("Anmeldung erfolgreich");
            }
            else
            {
                labelInformSuccessful.Text = "Name nicht gefunden oder Passwort fehlerhaft";
            }
        }

        private void PrepareMainPage(string message)
        {
            labelInformSuccessful.Text = message;
            App.IsUserLoggedIn = true;
            App.CurrentUser = person;
            Application.Current.MainPage = new MainPage();
        }
    }
}