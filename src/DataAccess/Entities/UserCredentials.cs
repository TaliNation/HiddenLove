namespace HiddenLove.DataAccess.Entities
{
    public class UserCredentials
    {
        public long Id { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
    }
}