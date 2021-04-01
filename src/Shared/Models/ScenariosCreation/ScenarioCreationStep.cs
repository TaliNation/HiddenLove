using System;

namespace HiddenLove.Shared.Models.ScenarioCreation
{
    public class ScenarioCreationStep
    {
        public int? StepId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int OrderIndex { get; set; }
    }
}