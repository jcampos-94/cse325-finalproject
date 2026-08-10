using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace cse325_finalproject.Models;

public class ApplicationUser : IdentityUser
{
  // Name displayed for the authenticated user in the application.
  [Required]
  [StringLength(50)]
  public string DisplayName { get; set; } = string.Empty;
}