using FlavorShare.interfaces;
using FlavorShare.Model;
using FlavorShare.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FlavorShare.ViewModel
{
    public class SigninViewModel : BaseViewModel
    {
        #region Global Variables
        private SQLiteConnection sqliteConnection;
        #endregion

        #region Properties
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(nameof(UserName)); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand SigninCmd { get; set; }
        public ICommand SignUpCmd { get; set; }
        #endregion

        #region Constructer
        public SigninViewModel()
        {
            SigninCmd = new Command(SignIn);
            SignUpCmd = new Command(SignUp);
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
        }

        #endregion

        #region Private Functions
        private async void SignUp()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Signup());
        }
        private async void SignIn()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Incomplete input", "Fields cannot be empty", "Ok");
            }
            else
            {
                bool val = ValidateCreds();
                if (val)
                {
                    Preferences.Set("user", UserName);
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Incorrect username or password", "Ok");
                }
            }
        }
        private bool ValidateCreds()
        {
            bool status = false;
            User user = sqliteConnection.Table<User>().FirstOrDefault(elm => elm.Name == UserName);
            if (user != null && user.Password == Password)
            {
                status = true;
            }
            return status;
            //Check sqlite table
        }
        #endregion
    }
}
