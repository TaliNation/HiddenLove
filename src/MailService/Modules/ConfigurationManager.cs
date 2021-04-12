using System;
using HiddenLove.MailService.Configurations;
using Microsoft.Extensions.Configuration;

namespace HiddenLove.MailService.Modules
{
	public static class ConfigurationManager
	{
		public static T Create<T>() where T : IConfigurationTemplate
		{
			IConfigurationRoot config = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json").Build();

			IConfigurationSection section = config.GetSection(nameof(T));
			return section.Get<T>();
		}
	}
}