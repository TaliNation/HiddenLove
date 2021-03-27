using System.ComponentModel.DataAnnotations;

namespace HiddenLove.Shared.Models.Authentication
{
    public class AuthenticationRequest
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}