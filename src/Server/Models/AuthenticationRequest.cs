using System.ComponentModel.DataAnnotations;

namespace HiddenLove.Server.Models
{
    public class AuthenticationRequest
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}