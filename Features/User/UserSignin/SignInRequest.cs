using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class SignInRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = "";
}