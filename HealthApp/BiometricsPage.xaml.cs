using FoodDataCentral.Models;
using HealthApp.Models;
using HealthApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BiometricsPage : ContentPage
    {
        public readonly DBService<User> Db;
        public UserService UserService { get; set; }
        public BiometricsPage(DBService<User> Db)
        {
            InitializeComponent();
            this.Db = Db;
            UserService = new UserService();
        }

        protected override bool OnBackButtonPressed()
        {
            return true; //disables back button
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                User user = new User();
                //Name
                user.Name = name.Text;
                //Sex
                user.Sex = sex.SelectedItem.ToString();
                //Age
                user.Age = int.Parse(age.Text);
                if (user.Age > 120 || user.Age < 18)
                    throw new Exception();
                //Weight
                user.Weight = Convert.ToDouble(weight.Text);
                //Height
                user.Height = Convert.ToDouble(height.Text);
                //Activity Level
                user.ActivityLevel = activityLevel.SelectedIndex;
                //Calorie Goal
                user.CalorieGoal = UserService.CalculateCalorieGoal(user);
                //Make new food log
                user.FoodLog = new Dictionary<DateTime, ObservableCollection<Food>>();
                //Make new calorie log
                user.CaloriesConsumedLog = new Dictionary<DateTime, int>();
                //Upload user data to database
                Db.InsertUser(user);
                await Navigation.PushAsync(new MainPage(user, Db));
            } catch(Exception w)
            {
                invalidLabel.TextColor = Color.Red;
                Console.WriteLine(w);
            }
        }
    }
}