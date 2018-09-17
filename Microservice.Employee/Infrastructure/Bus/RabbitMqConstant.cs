﻿namespace Microservice.Employee.Infrastructure.Bus
{
	internal class RabbitMqConstant
	{
		public const string RabbitMqUri = "amqp://dev:&T3rm0p1l4s&@192.168.0.9:5672";
		public const string JsonMimeType = "application/json";
		public const string Password = "guest";
		public const string UserName = "dev";


		public const string RegisterEmployeeCreatedQueue = "registeremployeecreated.service";

		public const string RegisterOrderserviceQueue = "registerorder.service";
		public const string NotificationService = "notification.service";
		public const string FinanceServiceQueue = "finance.service";
		public static string RegisterOrderserviceExchange = "registerorder.exchange";
	}
}