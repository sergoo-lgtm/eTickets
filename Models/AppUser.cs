using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;


public class AppUser
{
    [Key]
    public int Id { get; private set; }

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, ErrorMessage = "Full name must be less than 100 characters")]
    public string FullName { get;private set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format (example: user@example.com)")]
    public string Email { get;private set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [StringLength(40, MinimumLength = 8,ErrorMessage = "Password must be between 8 and 40 characters")]
    public string Password { get;private set; }

    public string? Role { get;private set; }
    
    private AppUser() { }


    public AppUser(string fullName, string email, string password, string role)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        Role = role;
    }
}
