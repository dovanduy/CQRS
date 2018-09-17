using Microservice.Messaging;
using System;

namespace Microservice.Notification
{
	public class EmployeeCreatedConsumer
	{
		public void Consume(IEmployeeCreated registeredEvent)
		{
			Console.WriteLine("Employee added notification sent employee id" + $"{registeredEvent.EmployeeID} registerd First Name:" + $"{registeredEvent.FirstName}");
		}
	}
}
