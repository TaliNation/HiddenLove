using System.Security;

namespace HiddenLove.MailService.Configurations
{
	public class MailSender : IConfigurationTemplate
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string Address { get; set; }
		public SecureString Password { get; set; }
	}
}