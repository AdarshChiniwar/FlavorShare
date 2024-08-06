using FlavorShare.Model;
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
    public partial class EditReciepe : ContentPage
    {
        public EditReciepe(FoodReceipe obj)
        {

            InitializeComponent();
            EditReciepeViewModel editReciepeViewModel = new EditReciepeViewModel(obj);
            this.BindingContext = editReciepeViewModel;
        }
    }
}