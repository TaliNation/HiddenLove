using HiddenLove.DataAccess.Entities;

namespace HiddenLove.Server.Models
{
    public class AuthenticationResponse
    {        
        public long Id { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string FullUserName { get; set; }
        public string PasswordHash { get; set; }
        public long? IdOffer { get; set; }
        public long? IdPrivilege { get; set; }
        public string Token { get; set; }

        public AuthenticationResponse(User user, string token)
        {
            Id = user.Id;
            EmailAddress = user.EmailAddress;
            UserName = user.UserName;
            FullUserName = user.FullUserName;
            PasswordHash = user.PasswordHash;
            IdOffer = user.IdOffer;
            IdPrivilege = user.IdPrivilege;
            Token = token;
        }
    }
}