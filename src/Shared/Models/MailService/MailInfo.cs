using System;

namespace HiddenLove.Shared.Models.MailService
{
    public class MailInfo
    {
        public string EmailAddress { get; set; }
        public string MailAddress { get; set; }
        public string MailSender { get; set; }
        public string MailBody { get; set; }
        public string MailSubject { get; set; }
        public DateTime SendTime { get; set; }
    }
}