using FoodDataCentral.Models;
using HealthApp.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HealthApp.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int ActivityLevel { get; set; }
        public Dictionary<DateTime, ObservableCollection<Food>> FoodLog { get; set; }
        public Dictionary<DateTime, int> CaloriesConsumedLog { get; set; }
        public string Email { get; set; }
        public int CalorieGoal { get; set; }
    }
}
