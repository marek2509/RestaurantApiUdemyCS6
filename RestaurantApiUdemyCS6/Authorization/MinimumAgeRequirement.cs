using Microsoft.AspNetCore.Authorization;

namespace RestaurantApiUdemyCS6.Authorization
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
            public int MinimumAge { get; }
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
