using Microsoft.AspNetCore.Authorization;
using RestaurantApiUdemyCS6.Entities;
using System.Security.Claims;

namespace RestaurantApiUdemyCS6.Authorization
{
    public class MinimumRestaurantRequirementHandler : AuthorizationHandler<MinimumRestaurantRequirement>
    {

        private readonly RestaurantDbContext _dbContext;
        public MinimumRestaurantRequirementHandler(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MinimumRestaurantRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdRestaurantCount = _dbContext.Restaurants.Count(x => x.CreatedById == userId);

            if(createdRestaurantCount >= requirement.MinimumRestaurant)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
