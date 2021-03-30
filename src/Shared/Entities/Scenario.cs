using System;

namespace HiddenLove.Shared.Entities
{
    public record Scenario : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public int IdUser { get; set; }
        public int IdScenarioTemplate { get; set; }
    }
}