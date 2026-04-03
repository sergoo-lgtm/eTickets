using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eTickets.Models;

public class Actor
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Full Name")]
    public string FullName { get; set; }
    [Display(Name = "Profile Picture URL")]
    public string? ProfilePivture { get; set; }
    [Display(Name = "Biography")]
    public string Bio { get; set; }
    [ValidateNever]
    public List<Actor_Movie>  Actors_Movies { get; set; }
    
}