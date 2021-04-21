using System;

namespace HiddenLove.Shared.Models
{
    public class UserScenarioData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsFinished => EventDate < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
        public bool IsToday => EventDate == DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
    }
}