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
    public partial class LoadingPage : ContentPage
    {
        private readonly DBService<User> Db;
        public LoadingPage()
        {
            InitializeComponent();
            Db = new DBService<User>();
            LoadPage();
        }

        private async void CheckForUser()
        {
            if (Db.UserExists())
            {
                await Navigation.PushAsync(new MainPage(Db.GetUser(), Db));
            }
            else
            {
                await Navigation.PushAsync(new WelcomePage(Db));
            }
        }

        public void LoadPage()
        {
            Db.ConnectToDb();
            CheckForUser();
        }
    }
}