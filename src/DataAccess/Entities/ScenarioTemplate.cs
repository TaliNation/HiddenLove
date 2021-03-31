using System;
using System.Collections.Generic;

#nullable disable

namespace HiddenLove.DataAccess.Entities
{
    public partial class ScenarioTemplate : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
