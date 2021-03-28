namespace HiddenLove.Shared.Entities
{
    public record User : IEntity<int>
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string FullUserName { get; set; }
        public string PasswordHash { get; set; }
        public long? IdOffer { get; set; }
        public long? IdPrivilege { get; set; }
    }
}