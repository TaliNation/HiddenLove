using System.ComponentModel.DataAnnotations;

namespace HiddenLove.Shared.Models
{
    public record RegisterRequest
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}