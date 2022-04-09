using HealthApp.Models;
using HealthApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly DBService<User> Db;
        public UserService UserService { get; set; }
        public User User { get; set; }
        public HomePage(User User, DBService<User> Db, UserService UserService)
        {
            InitializeComponent();
            this.Db = Db;
            this.User = User;
            this.UserService = UserService;
            SetCaloriesConsumed();
            BindingContext = User;
            SetCaloriesLeft();
            UpdateProgressBar();
        }
        private void SetCaloriesLeft()
        {
            if (User.CalorieGoal - User.CaloriesConsumedLog[DateTime.Today] < 0)
                caloriesLeft.Text = Convert.ToString(0);
            else
                caloriesLeft.Text = Convert.ToString(User.CalorieGoal - User.CaloriesConsumedLog[DateTime.Today]);
        }
        private void SetCaloriesConsumed()
        {
            if (!User.CaloriesConsumedLog.ContainsKey(DateTime.Today))
                User.CaloriesConsumedLog.Add(DateTime.Today, 0);
            caloriesConsumed.Text = Convert.ToString(User.CaloriesConsumedLog[DateTime.Today]);
        }

        public void UpdateProgressBar()
        {
            progressBar.Progress = Convert.ToDouble(User.CaloriesConsumedLog[DateTime.Today]) / Convert.ToDouble(User.CalorieGoal);
        }
        public void OnFoodLogChanged()
        {
            Console.WriteLine("Food has been logged");
            SetCaloriesLeft();
            SetCaloriesConsumed();
            UpdateProgressBar();
        }
    }
}