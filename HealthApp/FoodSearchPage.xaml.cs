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
    public partial class FoodSearchPage : ContentPage
    {
        private FoodDataService FoodDataService { get; set; }
        private readonly DBService<User> Db;
        public event Action FoodLogUpdated;
        public User User { get; set; }
        private ObservableCollection<SearchResultFood> Foods { get; set; }
        public FoodSearchPage(User user, DBService<User> Db)
        {
            InitializeComponent();
            this.User = user;
            this.Db = Db;
            FoodDataService = new FoodDataService("Enter USDA FoodData Central API key here");
            Foods = new ObservableCollection<SearchResultFood>();
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
            var searchText = searchBar.Text;
            var searchResult = await FoodDataService.FoodsSearch(searchText);
            while(searchResult == null) { } //Wait until searchResult is returned
            Console.WriteLine("Found foods");
            Foods.Clear();
            for(int i = 0; i < searchResult.Foods.Length; i++)
            {
                Foods.Add(searchResult.Foods[i]);
            }
            listView.ItemsSource = Foods;
            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var searchResultFood = e.SelectedItem as SearchResultFood;
            var page = new FoodDetailPage(FoodDataService, Db, User, searchResultFood.FdcId);
            page.FoodLogUpdated += OnFoodLogUpdated;
            await Navigation.PushAsync(page);
            listView.SelectedItem = null; //get rid of highlight
        }
        private void OnFoodLogUpdated()
        {
            FoodLogUpdated?.Invoke();
        }
    }
}
