using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace cse325_finalproject.Models;

public class ApplicationUser : IdentityUser
{
  [Required]
  [StringLength(50)]
  public string DisplayName { get; set; } = string.Empty;
}