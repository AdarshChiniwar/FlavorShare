using FlavorShare.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlavorShare
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
           this.CurrentPageChanged += OnCurrentPageChanged;
        }

        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            if (CurrentPage is NavigationPage navigationPage)
            {
                if (navigationPage.CurrentPage.BindingContext is IRefreshable refreshablePage)
                {
                    refreshablePage.Refresh();
                }
            }
        }
    }
}
