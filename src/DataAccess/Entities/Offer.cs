using System;
using System.Collections.Generic;

#nullable disable

namespace HiddenLove.DataAccess.Entities
{
    public partial class Offer : IEntity<int>
    {
        public int Id { get; set; }
        public string Designation { get; set; }
    }
}
