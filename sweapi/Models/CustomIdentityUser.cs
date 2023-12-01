using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace sweapi.Models
{
    public class CustomIdentityUser : IdentityUser
    {
        [PersonalData]
        [Display(Name ="Nombre")]
        public string? Nombre { get; set; }
    }
}
