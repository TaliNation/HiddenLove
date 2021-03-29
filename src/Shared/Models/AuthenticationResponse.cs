namespace HiddenLove.Shared.Models
{
    public record AuthenticationResponse
    {        
        public long Id { get; set; }
        public string Token { get; set; }
    }
}