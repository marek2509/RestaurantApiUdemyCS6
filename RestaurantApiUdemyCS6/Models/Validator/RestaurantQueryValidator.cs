using FluentValidation;

namespace RestaurantApiUdemyCS6.Models.Validator
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSize = new int[] {5, 10, 15};
        public RestaurantQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSize.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in " +
                        $"[{string.Join(",", allowedPageSize)}]");
                }
            });

        }
    }
}
