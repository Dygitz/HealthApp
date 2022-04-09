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
    public partial class WelcomePage : ContentPage
    {
        private readonly DBService<User> Db;
        public WelcomePage(DBService<User> Db)
        {
            InitializeComponent();
            this.Db = Db;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BiometricsPage(Db));
        }
    }
}