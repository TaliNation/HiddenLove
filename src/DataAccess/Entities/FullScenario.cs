using System;
using System.Collections.Generic;

namespace HiddenLove.DataAccess.Entities
{
	public partial class FullScenario : IEntity<int>
	{
		public string EmailAddress { get; set; }
		public int Id_Scenario { get; set; }
		public int Id_User { get; set; }
		public int Id_ScenarioTemplate { get; set; }
		public int Id_ScenarioTemplate_StepTemplate { get; set; }
		public int Id_StepTemplate { get; set; }
		public string Image { get; set; }
		public string ScenarioTitle { get; set; }
		public string ScenarioDescription { get; set; }
		public DateTime ScenarioEventDate { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string StepTitle { get; set; }
		public string StepDescription { get; set; }
		public string MailSender { get; set; }
		public string MailSubject { get; set; }
		public string MailBody { get; set; }
	}
}