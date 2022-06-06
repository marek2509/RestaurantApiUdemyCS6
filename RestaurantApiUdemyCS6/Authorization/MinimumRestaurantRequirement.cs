using Microsoft.AspNetCore.Authorization;

namespace RestaurantApiUdemyCS6.Authorization
{
    public class MinimumRestaurantRequirement : IAuthorizationRequirement
    {
        public int MinimumRestaurant { get; }
        public MinimumRestaurantRequirement(int minimumRestaurant)
        {
            MinimumRestaurant = minimumRestaurant;
        }
    }
}
