using FluentValidation;
using RestaurantApiUdemyCS6.Entities;

namespace RestaurantApiUdemyCS6.Models.Validator
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSize = new int[] {5, 10, 15};
        private string[] allowedSortByColumnNames = {nameof(Restaurant.Name),
        nameof(Restaurant.Category), nameof(Restaurant.Description)};
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
            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value)
                || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [" +
                $"{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
