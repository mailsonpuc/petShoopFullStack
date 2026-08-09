using System.ComponentModel.DataAnnotations;

namespace PetShoop.Infrastructure.Identity.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Login is required")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
