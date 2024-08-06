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
    public partial class AddReciepes : ContentPage
    {
        public AddReciepes()
        {
            InitializeComponent();
            AddReciepsViewModel addReciepsViewModel = new AddReciepsViewModel();
            this.BindingContext = addReciepsViewModel;
        }
    }
}