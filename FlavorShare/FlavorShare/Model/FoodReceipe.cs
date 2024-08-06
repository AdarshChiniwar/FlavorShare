using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlavorShare.Model
{
    public class FoodReceipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string recipeName { get; set; }
        public string description { get; set; }
        public string cuisine { get; set; }
        public string category { get; set; }
        public string preparationTime { get; set; }
        public string cookingTime { get; set; }
        public string totalTime { get; set; }
        public string difficultyLevel { get; set; }
        public string ingredients { get; set; }
        public string instructions { get; set; }
        public string dietType { get; set; }
    }

    public class ingredient
    {
        public string name { get; set; }
        public string quantity { get; set; }
        //public string unit { get; set; }
    }

    public class instructionClass
    {
        public string stepNumber { get; set; }
        public string instruction { get; set; }
    }

  
}
