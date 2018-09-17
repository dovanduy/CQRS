using MassTransit;
using MassTransit.RabbitMqTransport;
using System;

namespace Microservice.Employee.Infrastructure.Bus
{
	public static class BusConfigurator
	{
		public static IBusControl ConfigureBus(
			Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
				registrationAction = null)
		{

			return MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
			{
				var host = cfg.Host(new Uri(RabbitMqConstant.RabbitMqUri), hst =>
				{
					hst.Username(RabbitMqConstant.UserName);
					hst.Password(RabbitMqConstant.Password);
				});

				registrationAction?.Invoke(cfg, host);
			});
		}


	}
}