using System.ComponentModel.DataAnnotations;

namespace HiddenLove.Shared.Models
{
    public record AuthenticationRequest
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}