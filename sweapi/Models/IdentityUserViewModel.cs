using System.ComponentModel.DataAnnotations;

namespace sweapi.Models
{
    public class IdentityUserViewModel
    {
        public string Id { get; set; }
        public string UderName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public string Nombrecompleto { get; set; }
        public string Rol { get; set; }
    }
}
