using System;
using HiddenLove.Shared.Modules;

namespace HiddenLove.Shared.Models.ScenarioCreation
{
    public class ScenarioCreationStep
    {
        public int StepId
        {
            get => StepIdAsString.ToInt();
        }
        public string StepIdAsString { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}