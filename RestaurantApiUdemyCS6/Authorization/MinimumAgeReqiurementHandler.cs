using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantApiUdemyCS6.Authorization
{
    public class MinimumAgeReqiurementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeReqiurementHandler> _logger;

        public MinimumAgeReqiurementHandler(ILogger<MinimumAgeReqiurementHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
           var dateOfBirth = DateTime.Parse(context.User
                .FindFirst(c => c.Type == "DateOfBirth").Value);

            var userEmail = context.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;

            _logger.LogInformation($"User: {userEmail} with date of birth [{dateOfBirth}]");

            if(dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                _logger.LogInformation("Authorization successed");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed");
            }

            return Task.CompletedTask;
        }
    }
}
