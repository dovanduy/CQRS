using MassTransit;
using Microservice.Messaging;
using Microservice.Registration.Service.Message;
using System;
using System.Threading.Tasks;

namespace Microservice.Registration.Service
{
	class EmployeeCreatedConsumer : IConsumer<IEmployeeCreated>
	{
		public void Consume(IRegisterOrderCommand command)
		{
			//store order registration in db
			var id = 12;

			Console.WriteLine($"Order with id {id} registered");

			//notify suscribers
			var orderRegisteredEvent = new OrderRegisteredEvent(command, id);
			//publish event
		}

		public async Task Consume(ConsumeContext<IEmployeeCreated> context)
		{
			//store order registration in db
			var command = context.Message;
			var id = 12;

			Console.WriteLine($"Order with id {id} registered");


			//notify suscribers
			//var orderRegisteredEvent = new OrderRegisteredEvent(command, id);
			//publish event
			//await context.Publish<IOrderRegisteredEvent>(orderRegisteredEvent);
		}
	}
}
