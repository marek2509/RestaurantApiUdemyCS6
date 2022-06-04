using RestaurantApiUdemyCS6.Entities;

namespace RestaurantApiUdemyCS6
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Roles.Any())
                {
                   var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }


                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User",
                },
                new Role()
                {
                     Name = "Manager",
                },
                new Role()
                {
                     Name = "Admin",
                },
            };

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for kentucky Friend Chicken)",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "500 500 500",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Price = 10.30M,
                            Description = "goog food",
                        },

                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 5.30M,
                            Description = "like it",
                        }
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },

                new Restaurant()
                {
                    Name = "MC Donalds",
                    Category = "Fast Food",
                    Description = "Mc Donalds is good",
                    ContactEmail = "contact@mcdonalds.com",
                    ContactNumber = "588 777 475",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Wieśmac",
                            Price = 15.10M,
                            Description = "big and light",
                        },

                        new Dish()
                        {
                            Name = "Nuggets",
                            Price = 8.90M,
                            Description = "chicken is this",
                        }
                    },
                    Address = new Address()
                    {
                        City = "Białystok",
                        Street = "Produkcyjna 65",
                        PostalCode = "15-655"
                    }
                }
            };

            return restaurants;
        }
    }
}
