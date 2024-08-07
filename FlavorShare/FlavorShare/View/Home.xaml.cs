using FlavorShare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlavorShare.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            name.Text = Preferences.Get("user", "Guest");
            HomePageViewModel homePageViewModel = new HomePageViewModel();  
            this.BindingContext = homePageViewModel;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool result = await Application.Current.MainPage.DisplayAlert("Info", "Are you sure you want to logout?", "Ok", "Cancel");
            if (result)
            {
                Preferences.Set("user", string.Empty);
                Application.Current.MainPage = new NavigationPage(new Signin());
            }
        }
    }
}