using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Microsoft.Extensions.Configuration;
using HiddenLove.MailService.Configurations;
using HiddenLove.MailService.Modules;
using HiddenLove.Shared.Modules;

namespace HiddenLove.MailService
{
    internal static class Program
    {
		public static SmtpClient SmtpClient { get; private set; }

        private static async Task Main()
        {
            var emailSenderConfig = ConfigurationManager.Create<MailSender>();
			SmtpClient = new SmtpClient(emailSenderConfig.Host, emailSenderConfig.Port)
			{
				Credentials = new NetworkCredential(emailSenderConfig.Address, emailSenderConfig.Password),
				EnableSsl = true
			};

			var standardSchedulerFactory = new StdSchedulerFactory();

			while(true)
			{
				IScheduler scheduler = await standardSchedulerFactory.GetScheduler();

				await scheduler.Start();

				await ScheduleJobs(scheduler);

				// Formule permettant de recommancer le planificateur à minuit pile.
				int millisecondsUntilMidnight = TimeCalculator.MillisecondsUntil(DateTime.Parse(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));

				await Task.Delay(millisecondsUntilMidnight);

				// Une fois minuit atteint
				await scheduler.Shutdown();
			}
        }

		private static async Task ScheduleJobs(IScheduler scheduler)
		{
			//TODO : récupérer la liste des evenements.
			//TODO : créer la liste de mails.
			//TODO : attacher les jobs.
		}
    }
}
