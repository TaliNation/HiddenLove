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
using HiddenLove.DataAccess.Repositories;
using HiddenLove.DataAccess.TableAccesses;
using HiddenLove.DataAccess.Entities;
using System.Linq;
using HiddenLove.Shared.Models.MailService;
using HiddenLove.MailService.Jobs;

namespace HiddenLove.MailService
{
    internal static class Program
    {
		public static SmtpClient SmtpClient { get; private set; }
		public static List<MailMessage> EmailsToSendToday { get; private set; }

        private static async Task Main()
        {
			IConfigurationRoot config = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.Build();

			var emailSenderConfig = config.GetSection("MailSender").Get<MailSender>();
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
			EmailsToSendToday = new List<MailMessage>();

			var dbAccess = new Repository(new FullScenarioTableAccess());
			var entities = dbAccess.GetAll<int, FullScenario>();
			var mailInfos = entities.Where(x => x.StartDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")).Select(x => new MailInfo {
				EmailAddress = x.EmailAddress,
				SendTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " " + x.StartDate.ToString("HH:mm")),
				MailSender = x.MailSender,
				MailBody = x.MailBody,
				MailSubject = x.MailSubject
			});

			int i = 0;
			foreach(MailInfo mailInfo in mailInfos)
			{
				mailInfo.MailAddress = "cassykat@mail.com";
				mailInfo.MailSender ??= "";
				mailInfo.MailBody ??= "";
				mailInfo.MailSubject ??= "";

				EmailsToSendToday.Add(new MailMessage(
					new MailAddress(mailInfo.MailAddress, mailInfo.MailSender, Encoding.UTF8),
					new MailAddress(mailInfo.EmailAddress))
				{
					Body = mailInfo.MailBody,
					IsBodyHtml = true,
					BodyEncoding = Encoding.UTF8,
					Subject = mailInfo.MailSubject,
					SubjectEncoding = Encoding.UTF8
				});

				IJobDetail job = JobBuilder.Create<MailSenderJob>()
					.Build();

				job.JobDataMap["mailIndex"] = i;

				ITrigger trigger = TriggerBuilder.Create()
					.StartAt(mailInfo.SendTime)
					.ForJob(job)
					.Build();

				await scheduler.ScheduleJob(job, trigger);

				Console.WriteLine($"{mailInfo.EmailAddress} {mailInfo.SendTime}");

				i++;
			}

			Console.WriteLine("Scheduler ready! Mails to send today:");
			foreach(MailInfo mailInfo in mailInfos)
				Console.WriteLine($"{mailInfo.MailSubject}: {mailInfo.SendTime.ToString("HH:mm")}");
		}
    }
}
