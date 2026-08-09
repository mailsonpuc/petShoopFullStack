using System.ComponentModel.DataAnnotations;

namespace PetShoop.Infrastructure.Identity.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Login is required")]
    public string? UserName { get; set; }


    [EmailAddress(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

}
