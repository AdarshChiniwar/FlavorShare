using FlavorShare.interfaces;
using FlavorShare.Model;
using FlavorShare.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FlavorShare.ViewModel
{
    public class ReciepeListViewModel : BaseViewModel, IRefreshable
    {
        #region Properties
        private ObservableCollection<FoodReceipe> myRecieps;

        public ObservableCollection<FoodReceipe> MyRecieps
        {
            get { return myRecieps; }
            set { myRecieps = value; OnPropertyChanged(nameof(MyRecieps)); }
        }

        public List<FoodReceipe> MyReciepsList { get; set; }
        public ICommand EditCmd { get; set; }
        public ICommand DeleteCmd { get; set; }
        public ICommand RefreshCmd {  get; set; }
        #endregion

        #region Constructor
        public ReciepeListViewModel()
        {
            MyRecieps = new ObservableCollection<FoodReceipe>();
            MyReciepsList = new List<FoodReceipe>();
            EditCmd = new Command<FoodReceipe>(EditRecord);
            DeleteCmd = new Command<FoodReceipe>(DeleteRecord);
            RefreshCmd = new Command(RefreshData);
            Refresh();
        }



        #endregion

        #region Functions
        private void RefreshData(object obj)
        {
            Refresh();
        }

        private async void DeleteRecord(FoodReceipe obj)
        {
            try
            {
                int result = sqliteConnection.Delete(obj);
                if (result == 1)
                {
                    Refresh();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong!", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void EditRecord(FoodReceipe obj)
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EditReciepe( obj));
                //Refresh();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        public async void Refresh()
        {
            try
            {
                MyRecieps.Clear();
                MyReciepsList.Clear();
                var details = (from x in sqliteConnection.Table<FoodReceipe>() select x).ToList();

                if (details.Count == 0)
                {
                    //await Application.Current.MainPage.DisplayAlert("Info", "No records available!", "OK");
                    //return;
                }
                foreach (var item in details)
                {
                    MyReciepsList.Add(item);
                    MyRecieps.Add(item);
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }
        #endregion

    }
}
