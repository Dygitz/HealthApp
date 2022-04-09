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
    public partial class MainPage : TabbedPage
    {
        private readonly DBService<User> Db;
        public UserService UserService { get; set; }
        public User User { get; set; }
        public MainPage(User user, DBService<User> Db)
        {
            InitializeComponent();
            this.Db = Db;
            this.User = user;
            UserService = new UserService();
            var homePage = new HomePage(user, Db, UserService);
            homePage.Title = "Home";
            var logPage = new LogPage(user, Db, UserService);
            logPage.Title = "Log";
            logPage.FoodLogChanged += homePage.OnFoodLogChanged;
            this.Children.Add(homePage);
            this.Children.Add(logPage);
        }
        protected override bool OnBackButtonPressed()
        {
            return true; //disables back button
        }
    }
}