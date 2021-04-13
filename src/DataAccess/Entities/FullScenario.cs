using System;
using System.Collections.Generic;

namespace HiddenLove.DataAccess.Entities
{
	public partial class FullScenario : IEntity<int>
	{
		public int IdScenario { get; set; }
		public int IdUser { get; set; }
		public int IdScenarioTemplate { get; set; }
		public int IdScenarioTemplateStepTemplate { get; set; }
		public int IdStepTemplate { get; set; }
		public string Image { get; set; }
		public string ScenarioTitle { get; set; }
		public string ScenarioDescription { get; set; }
		public DateTime Eventdate { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string StepTitle { get; set; }
		public string StepDescription { get; set; }
	}
}