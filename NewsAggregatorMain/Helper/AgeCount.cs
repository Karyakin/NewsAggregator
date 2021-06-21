using Entities.Entity.Users;
using System;

namespace NewsAggregatorMain.Helper
{
    public static class AgeCount
    {
        public static string GetAge(User user)
        {
            // Save today date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - user.DayOfBirth.Year;
            // Go back to the year the person was born in case of a leap year
            if (user.DayOfBirth > today.AddYears(-age)) 
                age--;
            return age.ToString();
        }
    }
}
