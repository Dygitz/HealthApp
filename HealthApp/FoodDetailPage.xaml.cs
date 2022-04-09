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
    public partial class FoodDetailPage : ContentPage
    {
        private FoodDataService FoodDataService { get; set; }
        private readonly DBService<User> Db;
        public event Action FoodLogUpdated;
        public User User { get; set; }
        private Food Food { get; set; }
        public Dictionary<NutrientType, FoodNutrient> FoodNutrients { get; set; }
        public float portionRatio { get; set; }
        public FoodDetailPage(FoodDataService FoodDataService, DBService<User> Db, User User, int fdcId)
        {
            InitializeComponent();
            this.FoodDataService = FoodDataService;
            this.Db = Db;
            this.User = User;
            Task.Run(() => GetFood(fdcId)).Wait();
            BindingContext = Food;
            float portionValue;
            if (Food.FoodPortions.Length != 0)
                portionValue = Food.FoodPortions[0].GramWeight;
            else
                portionValue = 100;
            portion.Text = portionValue + "g";
            portion.TextColor = Color.Black;
            portionRatio = portionValue / 100;
            calories.Text = Convert.ToString(Convert.ToInt32(portionRatio * Food.FoodNutrients[NutrientType.EnergyKcal].Amount));
            calories.TextColor = Color.Black;
            foreach(KeyValuePair<NutrientType, FoodNutrient> entry in Food.FoodNutrients)
            {
                try
                {
                    AddNutrientLabel(entry.Value.Nutrient.Name, entry.Value.Nutrient.Id);
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
        }
        private async Task GetFood(int fdcId)
        {
            var food = await FoodDataService.FoodDetailSearch(fdcId);
            while (food == null) { } //Wait until food is returned
            Food = food;
        }

        private void AddNutrientLabel(string nutrient, object id)
        {
            NutrientType nutrientEnum = (NutrientType)Enum.Parse(typeof(NutrientType), Enum.GetName(typeof(NutrientType), id));
            nutrients.Children.Add(new Label
            {
                Text = nutrient + ": " + Convert.ToString(Food.FoodNutrients[nutrientEnum].Amount * portionRatio) + Food.FoodNutrients[nutrientEnum].Nutrient.UnitName,
                TextColor = Color.Black,
                FontSize = 20
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //Update calories consumed
            User.CaloriesConsumedLog[DateTime.Today] += Convert.ToInt32(Food.FoodNutrients[NutrientType.EnergyKcal].Amount * portionRatio);
            //Update the database
            if (User.FoodLog.ContainsKey(DateTime.Today)) //Foods already exist for the current day
            {
                var foods = User.FoodLog[DateTime.Today];
                foods.Add(Food);
                User.FoodLog[DateTime.Today] = foods;
            }
            else //Foods don't already exist for the current day
            {
                var foods = new ObservableCollection<Food>();
                foods.Add(Food);
                User.FoodLog.Add(DateTime.Today, foods);
            }
            Db.UpdateUser(User);
            FoodLogUpdated?.Invoke();
            //Go back to the log page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();
        }
    }
}