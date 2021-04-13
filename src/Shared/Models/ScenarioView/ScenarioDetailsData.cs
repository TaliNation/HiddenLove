using System;
using System.Collections.Generic;

namespace Shared.Models.ScenarioView
{
    public class ScenarioDetailsData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public List<StepDetailsData> Steps { get; set; }
    }
}