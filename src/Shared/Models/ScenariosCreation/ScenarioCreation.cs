using System.Collections.Generic;

namespace HiddenLove.Shared.Models.ScenarioCreation
{
    public class ScenarioCreation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<ScenarioCreationStep> Steps { get; set; }
    }
}