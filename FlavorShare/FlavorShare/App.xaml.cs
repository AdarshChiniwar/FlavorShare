using FlavorShare.interfaces;
using FlavorShare.Model;
using FlavorShare.View;
using SQLite;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlavorShare
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            string userName = Preferences.Get("user", string.Empty);
            CreateTables();
            if (string.IsNullOrEmpty(userName))
            {
                MainPage = new NavigationPage(new Signin());
            }
            else
            {
                MainPage = new  NavigationPage(new MainPage());
            }
          
        }
        private void CreateTables()
        {
            SQLiteConnection sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
            sqliteConnection.CreateTable<User>();
            sqliteConnection.CreateTable<FoodReceipe>();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
