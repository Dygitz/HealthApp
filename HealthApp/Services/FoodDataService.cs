using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using FoodDataCentral;
using FoodDataCentral.Models;
using System.Collections;
using System.Threading.Tasks;

namespace HealthApp.Services
{
    public class FoodDataService
    {
        private string Key { get; set; }
        public FoodDataCentralAPI Api { get; set; }
        public SearchResultFood[] SearchResultFoods { get; set; }
        public Food Food { get; set; }
        public FoodDataService(string key)
        {
            Key = key;
            Api = new FoodDataCentralAPI(Key);
        }

        public Task<SearchResult> FoodsSearch(string searchTerm)
        {
            Console.WriteLine("Searching foods");
            return Api.Search(searchTerm);
        }

        public Task<Food> FoodDetailSearch(int fdcId)
        {
            Console.WriteLine("Searching for details on " + fdcId);
            return Api.GetFoodById(fdcId);
        }
    }
}
