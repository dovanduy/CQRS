using MassTransit;
using Microservice.Messaging;
using System;

namespace Microservice.Registration.Service
{
	class Program
	{
		static void Main(string[] args)
		{
			//RabbitMqManager manager = new RabbitMqManager();
			//   manager.ListenForRegisterOrderCommand();
			var bus = BusConfigurator.ConfigureBus((cfg, host) =>
			{
				cfg.ReceiveEndpoint(host, RabbitMqConstant.RegisterOrderserviceQueue, e =>
				 {
					 e.Consumer<RegisterOrderConsumer>();
					 e.Consumer<EmployeeCreatedConsumer>();
				 });



			});

			bus.StartAsync();
			Console.WriteLine("Hello World!");
			Console.ReadLine();
			bus.StopAsync();
		}
	}
}
