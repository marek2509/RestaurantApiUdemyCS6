using System.ComponentModel.DataAnnotations;

namespace RestaurantApiUdemyCS6.Models
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public int RoleId { get; set; } = 1;
    }
}
