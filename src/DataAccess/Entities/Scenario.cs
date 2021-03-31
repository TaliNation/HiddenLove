using System;
using System.Collections.Generic;

#nullable disable

namespace HiddenLove.DataAccess.Entities
{
    public partial class Scenario : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime Eventdate { get; set; }
        public int IdUser { get; set; }
        public int IdScenariotemplate { get; set; }

        public virtual ScenarioTemplate IdScenariotemplateNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
