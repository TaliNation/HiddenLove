using System;
using HiddenLove.DataAccess.Entities;

namespace HiddenLove.DataAccess.Entities
{
    public class ScenarioSchedule : IEntity<int>
    {
        public int IdUser { get; set; }
		public DateTime EventDate { get; set; }
		public int IdScenario { get; set; }
		public string Title { get; set; }
		public int IdScenarioTemplates { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
    }
}