using FlavorShare.interfaces;
using FlavorShare.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FlavorShare.ViewModel
{
    public class HomePageViewModel : BaseViewModel, IRefreshable
    {
        #region Properties
        private ObservableCollection<ImageModel> imageCollection;
        public ObservableCollection<ImageModel> ImageCollection
        {
            get { return imageCollection; }
            set { imageCollection = value; OnPropertyChanged(nameof(ImageCollection)); }
        }

        private ObservableCollection<FoodReceipe> myRecieps;

        public ObservableCollection<FoodReceipe> MyRecieps
        {
            get { return myRecieps; }
            set { myRecieps = value; OnPropertyChanged(nameof(MyRecieps)); }
        }

        private bool isVisibleText;

        public bool IsVisibleText
        {
            get { return isVisibleText; }
            set { isVisibleText = value; OnPropertyChanged(nameof(IsVisibleText)); }
        }

        #endregion
        #region Constructor
        public HomePageViewModel()
        {
            MyRecieps = new ObservableCollection<FoodReceipe>();
            ImageCollection = new ObservableCollection<ImageModel>();
            ImageCollection.Add(new ImageModel { CauroselImage = "a.jpg" });
            ImageCollection.Add(new ImageModel { CauroselImage = "b.jpeg" });
            ImageCollection.Add(new ImageModel { CauroselImage = "c.avif" });
            ImageCollection.Add(new ImageModel { CauroselImage = "d.webp" });
            ImageCollection.Add(new ImageModel { CauroselImage = "e.avif" });
            Refresh();
        }
        #endregion

        public async void Refresh()
        {
            try
            {
                MyRecieps.Clear();
               
                var details = (from x in sqliteConnection.Table<FoodReceipe>() select x).ToList().Take(1);
                if(details.Count() == 0)
                {
                    IsVisibleText = true;
                }
                else
                {
                    IsVisibleText = false;
                    foreach (var item in details)
                    {

                        MyRecieps.Add(item);
                    }
                }
               

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
