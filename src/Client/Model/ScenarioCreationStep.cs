using System;

namespace HiddenLove.Client.Models
{
    public class ScenarioCreationStep
    {
        public int StepId { get; set; }
        public int StepTitle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int OrderIndex { get; set; }
    }
}