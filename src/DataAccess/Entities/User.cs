using System;
using System.Collections.Generic;

#nullable disable

namespace HiddenLove.DataAccess.Entities
{
    public partial class User : IEntity<int>
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string FullUsername { get; set; }
        public string Passwordhash { get; set; }
        public int? IdPrivilege { get; set; }
    }
}
