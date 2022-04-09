using FoodDataCentral.Models;
using HealthApp.Models;
using LiteDB;
using MongoDB.Driver;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthApp.Services
{
    public class DBService<User>
    {
        public LiteDatabase db { get; set; }
        public ILiteCollection<User> Collection { get; set; }
        
        public void ConnectToDb()
        {
            db = new LiteDatabase(DependencyService.Get<ILiteDBAccess>().DatabasePath());
            Collection = db.GetCollection<User>("User");
        }

        public void DeleteUser()
        {
            Collection.DeleteAll();
        }

        private BsonValue GetCollectionId()
        {
            return new BsonValue(0); //arbitrary value because there is only 1 collection
        }

        public void InsertUser(User user)
        {
            Collection.Insert(GetCollectionId(), user);
        }

        public void UpdateUser(User user)
        {
            Collection.Update(GetCollectionId(), user);
        }

        public bool UserExists() => Collection.Count() == 1;

        public User GetUser()
        {
            return Collection.FindAll().ToList()[0];
        }
    }
}
