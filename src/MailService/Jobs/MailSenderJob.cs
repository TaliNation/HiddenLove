using System.Threading.Tasks;
using Quartz;
using System.Linq;
using System;

namespace HiddenLove.MailService.Jobs
{
	public class MailSenderJob : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			int mailIndex = context.JobDetail.JobDataMap.GetInt("mailIndex");
			var mail = Program.EmailsToSendToday[mailIndex];

			Program.SmtpClient.SendAsync(mail, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}_i{mailIndex}");

			Console.WriteLine($"Sent mail to {mail.To} with subject: {mail.Subject}");
		}
	}
}