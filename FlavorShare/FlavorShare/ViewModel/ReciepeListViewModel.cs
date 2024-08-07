using FlavorShare.interfaces;
using FlavorShare.Model;
using FlavorShare.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
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
        public ICommand ShareCmd { get; set; }
        #endregion

        #region Constructor
        public ReciepeListViewModel()
        {
            MyRecieps = new ObservableCollection<FoodReceipe>();
            MyReciepsList = new List<FoodReceipe>();
            EditCmd = new Command<FoodReceipe>(EditRecord);
            DeleteCmd = new Command<FoodReceipe>(DeleteRecord);
            RefreshCmd = new Command(RefreshData);
            ShareCmd = new Command<FoodReceipe>(ShareViaEmail);
            Refresh();
        }





        #endregion

        #region Functions
        private async void ShareViaEmail(FoodReceipe foodReceipe)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"Recipe Name: {foodReceipe.recipeName}");
                stringBuilder.AppendLine($"Description: {foodReceipe.description}");
                stringBuilder.AppendLine($"Cuisine: {foodReceipe.cuisine}");
                stringBuilder.AppendLine($"Category: {foodReceipe.category}");
                stringBuilder.AppendLine($"Preparation Time: {foodReceipe.preparationTime}");
                stringBuilder.AppendLine($"Cooking Time: {foodReceipe.cookingTime}");
                stringBuilder.AppendLine($"Total Time: {foodReceipe.totalTime}");
                stringBuilder.AppendLine($"Difficulty Level: {foodReceipe.difficultyLevel}");
                stringBuilder.AppendLine($"Diet Type: {foodReceipe.dietType}");
                List<ingredient> ingredients = JsonConvert.DeserializeObject<List<ingredient>>(foodReceipe.ingredients);
                List<instructionClass> ste = JsonConvert.DeserializeObject<List<instructionClass>>(foodReceipe.instructions);
                foreach (var item in ingredients)
                {
                    stringBuilder.AppendLine($"Ingredients: {item.name} and Qty {item.quantity} in gms");
                }
                foreach (var item in ste)
                {
                    stringBuilder.AppendLine($"Step: {item.stepNumber}) {item.instruction}");
                }
                //stringBuilder.AppendLine($"Ingredients: {foodReceipe.ingredients}");
                //stringBuilder.AppendLine($"Instructions: {foodReceipe.instructions}");

                string readableString = stringBuilder.ToString();
                var message = new EmailMessage
                {
                    Subject = $"Here is the reciepe for{foodReceipe.recipeName}" ,
                    Body = readableString,
                    To = new List<string>(),
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Xamarin.Essentials.Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", fbsEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
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
