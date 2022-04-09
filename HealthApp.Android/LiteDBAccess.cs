using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthApp.Droid;
using HealthApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(LiteDBAccess))]
namespace HealthApp.Droid
{
    class LiteDBAccess : ILiteDBAccess
    {
        public string DatabasePath()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Database");

            if (!File.Exists(path))
                File.Create(path).Dispose();

            return path;
        }
    }
}