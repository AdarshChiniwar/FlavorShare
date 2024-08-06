using FlavorShare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlavorShare.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signin : ContentPage
    {
        public Signin()
        {
            InitializeComponent();
            SigninViewModel signinViewModel = new SigninViewModel();
            this.BindingContext = signinViewModel;
        }
    }
}