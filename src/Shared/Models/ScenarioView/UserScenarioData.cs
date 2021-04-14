using System;

namespace HiddenLove.Shared.Models
{
    public class UserScenarioData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsFinished => EventDate < DateTime.Now.AddDays(1);
        public bool IsToday => EventDate == DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
    }
}