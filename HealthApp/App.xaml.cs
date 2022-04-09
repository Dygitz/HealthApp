using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthApp.Services;
using HealthApp.Models;

namespace HealthApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoadingPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
