using System;
using System.Collections.Generic;

#nullable disable

namespace HiddenLove.DataAccess.Entities
{
    public partial class ScenarioTemplatesStepTemplate : IEntity<int>
    {
        public int Id { get; set; }
        public int IdScenariotemplate { get; set; }
        public int IdSteptemplate { get; set; }
        public int? OrderIndex { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ScenarioTemplate IdScenariotemplateNavigation { get; set; }
        public virtual StepTemplate IdSteptemplateNavigation { get; set; }
    }
}
