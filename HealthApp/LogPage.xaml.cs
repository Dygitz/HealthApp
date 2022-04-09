using FoodDataCentral.Models;
using HealthApp.Models;
using HealthApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthApp
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogPage : ContentPage
    {
        public event Action FoodLogChanged;

        private readonly DBService<User> Db;
        public UserService UserService { get; set; }
        public User User { get; set; }
        public LogPage(User user, DBService<User> Db, UserService UserService)
        {
            InitializeComponent();
            this.Db = Db;
            this.User = user;
            this.UserService = UserService;
            if(User.FoodLog.ContainsKey(DateTime.Today))
            {
                listView.ItemsSource = User.FoodLog[DateTime.Today];
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var page = new FoodSearchPage(User, Db);
            page.FoodLogUpdated += OnFoodLogUpdated;
            await Navigation.PushAsync(page);
        }

        private void OnFoodLogUpdated()
        {
            if(listView.ItemsSource == null)
            {
                listView.ItemsSource = User.FoodLog[DateTime.Today];
            }
            FoodLogChanged?.Invoke();
        }

        private void delete_Clicked(object sender, EventArgs e)
        {
            //Food deleted
            var menuItem = sender as MenuItem;
            var food = menuItem.BindingContext as Food;
            User.FoodLog[DateTime.Today].Remove(food);
            //Get portion ratio
            float portionValue;
            if (food.FoodPortions.Length != 0)
                portionValue = food.FoodPortions[0].GramWeight;
            else
                portionValue = 100;
            var portionRatio = portionValue / 100;
            User.CaloriesConsumedLog[DateTime.Today] -= Convert.ToInt32(food.FoodNutrients[NutrientType.EnergyKcal].Amount * portionRatio);
            Db.UpdateUser(User);
            listView.ItemsSource = User.FoodLog[DateTime.Today];
            FoodLogChanged?.Invoke();
        }
    }
}