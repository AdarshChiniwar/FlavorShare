using FlavorShare.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FlavorShare.ViewModel
{
    public class EditReciepeViewModel : BaseViewModel
    {
        #region Global Members
        FoodReceipe FoodReceipe;
        #endregion
        #region Properties
        private string _recipeName;
        public string recipeName
        {
            get { return _recipeName; }
            set { _recipeName = value; OnPropertyChanged(nameof(recipeName)); }
        }

        private string _description;
        public string description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(description)); }
        }

        private string _cuisine;
        public string cuisine
        {
            get { return _cuisine; }
            set { _cuisine = value; OnPropertyChanged(nameof(cuisine)); }
        }

        private ObservableCollection<string> categorys;
        public ObservableCollection<string> Categorys
        {
            get { return categorys; }
            set { categorys = value; OnPropertyChanged(nameof(Categorys)); }
        }

        private string selectedCategory;

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set { selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); }
        }

        private string _cookingTime;
        public string cookingTime
        {
            get { return _cookingTime; }
            set { _cookingTime = value; OnPropertyChanged(nameof(cookingTime)); }
        }

        private string _preparationTime;
        public string preparationTime
        {
            get { return _preparationTime; }
            set { _preparationTime = value; OnPropertyChanged(nameof(preparationTime)); }
        }

        private string _totalTime;
        public string totalTime
        {
            get { return _totalTime; }
            set { _totalTime = value; OnPropertyChanged(nameof(totalTime)); }
        }

        private ObservableCollection<string> difficultys;

        public ObservableCollection<string> Difficultys
        {
            get { return difficultys; }
            set { difficultys = value; OnPropertyChanged(nameof(Difficultys)); }
        }

        private string selectedDifficulty;
        public string SelectedDifficulty
        {
            get { return selectedDifficulty; }
            set { selectedDifficulty = value; OnPropertyChanged(nameof(SelectedDifficulty)); }
        }

        private ObservableCollection<string> diets;

        public ObservableCollection<string> Diets
        {
            get { return diets; }
            set { diets = value; OnPropertyChanged(nameof(Diets)); }
        }
        private string selectedDiet;

        public string SelectedDiet
        {
            get { return selectedDiet; }
            set { selectedDiet = value; OnPropertyChanged(nameof(SelectedDiet)); }
        }

        private ObservableCollection<ingredient> ingredients;

        public ObservableCollection<ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; OnPropertyChanged(nameof(Ingredients)); }
        }

        private string ingredientsName;

        public string IngredientsName
        {
            get { return ingredientsName; }
            set { ingredientsName = value; OnPropertyChanged(nameof(IngredientsName)); }
        }

        private string ingredientsQuantity;

        public string IngredientsQuantity
        {
            get { return ingredientsQuantity; }
            set { ingredientsQuantity = value; OnPropertyChanged(nameof(IngredientsQuantity)); }
        }
        public ICommand AddIngredientCommand { get; set; }
        public ICommand AddStepsCommand { get; set; }

        public ICommand DeleteIngri { get; set; }

        private string stepNumber;

        public string StepNumber
        {
            get { return stepNumber; }
            set { stepNumber = value; OnPropertyChanged(nameof(StepNumber)); }
        }

        private string instruction;

        public string Instruction
        {
            get { return instruction; }
            set { instruction = value; OnPropertyChanged(nameof(Instruction)); }
        }

        private ObservableCollection<instructionClass> steps;

        public ObservableCollection<instructionClass> Steps
        {
            get { return steps; }
            set { steps = value; OnPropertyChanged(nameof(Steps)); }
        }
        public ICommand DeleteStep { get; set; }
        public ICommand EditCompleteReciepeCmd { get; set; }
        #endregion

        #region Constructor
        public EditReciepeViewModel(FoodReceipe foodReceipe)
        {
            FoodReceipe = foodReceipe;
            AddIngredientCommand = new Command(AddIngredients);
            EditCompleteReciepeCmd = new Command(EditCompeteReceipe);
            DeleteIngri = new Command<ingredient>(DeleteIng);
            DeleteStep = new Command<instructionClass>(DeleteStepFunc);
            AddStepsCommand = new Command(AddSteps);
            AddStepsCommand = new Command(AddSteps);
            Difficultys = new ObservableCollection<string>();
            Categorys = new ObservableCollection<string>();
            Ingredients = new ObservableCollection<ingredient>();
            Steps = new ObservableCollection<instructionClass>();
            Diets = new ObservableCollection<string>();
            Initialize();

        }
        #endregion

        #region Functions
        private async void EditCompeteReceipe()
        {
            try
            {
                if (string.IsNullOrEmpty(recipeName) || string.IsNullOrEmpty(description)
                    || string.IsNullOrEmpty(cuisine) || string.IsNullOrEmpty(SelectedCategory)
                    || string.IsNullOrEmpty(cookingTime) || string.IsNullOrEmpty(preparationTime)
                    || string.IsNullOrEmpty(totalTime) || string.IsNullOrEmpty(SelectedDifficulty)
                    || string.IsNullOrEmpty(SelectedDiet) || Ingredients.Count == 0 || Steps.Count == 0
                    )
                {
                    await Application.Current.MainPage.DisplayAlert("Incomplete input", "Fields cannot be empty", "Ok");
                }
                else
                {
                    FoodReceipe foodReceipe = new FoodReceipe()
                    {
                        Id = FoodReceipe.Id,
                        recipeName = recipeName,
                        description = description,
                        cuisine = cuisine,
                        category = SelectedCategory,
                        preparationTime = preparationTime,
                        totalTime = totalTime,
                        cookingTime = cookingTime,
                        difficultyLevel = SelectedDifficulty,
                        dietType = SelectedDiet,
                        ingredients = JsonConvert.SerializeObject(Ingredients),
                        instructions = JsonConvert.SerializeObject(Steps)
                    };
                    //foreach (var item in Ingredients)
                    //{
                    //    foodReceipe.ingredients.Add(item);
                    //}
                    //foreach (var item in Steps)
                    //{
                    //    foodReceipe.instructions.Add(item);
                    //}
                    int result = sqliteConnection.Update(foodReceipe);
                    if (result == 1)
                    {
                        //recipeName = string.Empty;
                        //description = string.Empty;
                        //cuisine = string.Empty;
                        //SelectedCategory = string.Empty;
                        //cookingTime = string.Empty;
                        //preparationTime = string.Empty;
                        //totalTime = string.Empty;
                        //SelectedDifficulty = string.Empty;
                        //SelectedDiet = string.Empty;
                        //Ingredients.Clear();
                        //Steps.Clear();
                        //IngredientsName = string.Empty;
                        //IngredientsQuantity = string.Empty;
                        //StepNumber = string.Empty;
                        //Instruction = string.Empty;
                        await Application.Current.MainPage.DisplayAlert("Error", "Receipe Updated!!", "Ok");
                        await Application.Current.MainPage.Navigation.PopAsync();

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Please try again", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private void DeleteStepFunc(instructionClass obj)
        {
            Steps.Remove(obj);
        }
        private void DeleteIng(ingredient obj)
        {
            Ingredients.Remove(obj);
        }

        private void Initialize()
        {
            Difficultys.Add("Easy");
            Difficultys.Add("Medium");
            Difficultys.Add("Hard");

            Categorys.Add("Main Course");
            Categorys.Add("Staters");
            Categorys.Add("Dessert");
            Categorys.Add("Side Dish");

            Diets.Add("Non Vegetarian");
            Diets.Add("Vegetarian");

            recipeName = FoodReceipe.recipeName;
            description = FoodReceipe.description;
            cuisine = FoodReceipe.cuisine;
            SelectedCategory = FoodReceipe.category;
            preparationTime = FoodReceipe.preparationTime;
            totalTime = FoodReceipe.totalTime;
            cookingTime = FoodReceipe.cookingTime;
            SelectedDifficulty = FoodReceipe.difficultyLevel;
            SelectedDiet = FoodReceipe.dietType;
            List<ingredient> ingredients = JsonConvert.DeserializeObject<List<ingredient>>(FoodReceipe.ingredients);
            List<instructionClass> ste = JsonConvert.DeserializeObject<List<instructionClass>>(FoodReceipe.instructions);
            foreach (var item in ingredients)
            {
                Ingredients.Add(item);
            }
            foreach (var item in ste)
            {
                Steps.Add(item);
            }

        }

        private async void AddSteps(object obj)
        {
            if (string.IsNullOrWhiteSpace(StepNumber) || string.IsNullOrEmpty(Instruction))
            {
                await Application.Current.MainPage.DisplayAlert("Incomplete input", "Check the Steps", "Ok");
            }
            else
            {
                Steps.Add(new instructionClass() { stepNumber = StepNumber, instruction = Instruction });
            }
        }
        private async void AddIngredients()
        {
            if (string.IsNullOrWhiteSpace(IngredientsQuantity) || string.IsNullOrEmpty(IngredientsName))
            {
                await Application.Current.MainPage.DisplayAlert("Incomplete input", "Check the Ingredients", "Ok");
            }
            else
            {
                Ingredients.Add(new ingredient() { name = IngredientsName, quantity = IngredientsQuantity });
            }
        }
        #endregion

    }
}
