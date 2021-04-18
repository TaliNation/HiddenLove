using System;
using System.Collections.Generic;

#nullable disable

namespace HiddenLove.DataAccess.Entities
{
    public partial class StepTemplate : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
		public int Id_FakeSpam { get; set; }
    }
}
