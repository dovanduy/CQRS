namespace Microservice.Messaging
{
	public static class RabbitMqConstant
	{
		public const string RabbitMqUri = "rabbitmq://192.168.0.9/fireonwheels/";
		public const string UserName = "dev";
		public const string Password = "&T3rm0p1l4s&";



		public const string RegisterEmployeeCreatedQueue = "registeremployeecreated.service";

		public const string RegisterOrderserviceQueue = "registerorder.service";
		public const string NotificationServiceQueue = "notification.service";
		public const string FinanceServiceQueue = "finance.service";
	}
}
