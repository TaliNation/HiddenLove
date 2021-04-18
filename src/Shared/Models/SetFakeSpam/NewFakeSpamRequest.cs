namespace HiddenLove.Shared.Models.SetFakeSpam
{
    public class NewFakeSpamRequest
    {
        public int StepTemplateId { get; set; }
		public string Name { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
    }
}