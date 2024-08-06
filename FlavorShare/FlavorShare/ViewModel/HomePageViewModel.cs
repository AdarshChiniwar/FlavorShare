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
            ImageCollection.Add(new ImageModel { CauroselImage = "https://static.vecteezy.com/system/resources/thumbnails/017/545/249/small/fast-food-banner-decorated-with-horizontal-border-of-doodles-and-lettering-quote-for-social-media-menues-prints-cards-templates-posters-etc-eps-10-free-vector.jpg" });
            ImageCollection.Add(new ImageModel { CauroselImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ_FkABbFMKkGN4OdSGOv7VxuI5Hi_UwVB-qg&s" });
            ImageCollection.Add(new ImageModel { CauroselImage = "https://img.freepik.com/free-vector/flat-horizontal-banner-template-world-vegan-day-celebration_23-2150898076.jpg" });
            ImageCollection.Add(new ImageModel { CauroselImage = "https://thumbs.dreamstime.com/b/banner-seafood-shrimps-their-preparation-pan-green-beans-delicious-healthy-food-horizontal-photo-advertising-169242604.jpg" });
            ImageCollection.Add(new ImageModel { CauroselImage = "https://img.freepik.com/free-photo/delicious-flexitarian-diet-arrangement-top-view_23-2148862677.jpg" });
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
