using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Registration.Service
{
    public static class RabbitMqConstant
    {
        public const string RabbitMqUri = "amqp://dev:&T3rm0p1l4s&@10.0.0.11:5672";
        public const string JsonMimeType = "application/json";
        public const string Password = "guest";
        public const string RegisterOrderserviceQueue = "registerorder.service";
        public const string NotificationService = "notification.service";
        public const string FinanceServiceQueue = "finance.service";
        public static string RegisterOrderserviceExchange = "registerorder.exchange";
    }
}
