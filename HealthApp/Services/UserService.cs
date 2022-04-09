using HealthApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthApp.Services
{
    public class UserService
    {
        public int CalculateCalorieGoal(User user)
        {
            var bmr = CalculateBMR(user);
            switch(user.ActivityLevel)
            {
                case 0:
                    return Convert.ToInt32(bmr * 1.2);
                case 1:
                    return Convert.ToInt32(bmr * 1.375);
                case 2:
                    return Convert.ToInt32(bmr * 1.55);
                case 3:
                    return Convert.ToInt32(bmr * 1.725);
                case 4:
                    return Convert.ToInt32(bmr * 1.9);
            }
            return 0;
        }
        private double CalculateBMR(User user)
        {
            //The Harris-Benedict Equation
            //Basal metabolic rate
            if (user.Sex == "Male")
            {
                return 66 + (6.2 * user.Weight) + (12.7 * user.Height) - (6.76 * user.Age);
            } 
            else //female
            {
                return 655.1 + (4.35 * user.Weight) + (4.7 * user.Height) - (4.7 * user.Age);
            }
        }
    }
}
