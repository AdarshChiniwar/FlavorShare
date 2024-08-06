using FlavorShare.interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace FlavorShare.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Global Variables
        public SQLiteConnection sqliteConnection { get; set; }
        #endregion
        public BaseViewModel()
        {
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
