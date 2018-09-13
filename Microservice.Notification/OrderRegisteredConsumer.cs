using System;
using System.Collections.Generic;
using System.Text;
using Microservice.Messaging;

namespace Microservice.Notification
{
    public class OrderRegisteredConsumer
    {
        public void Consume(IOrderRegisteredEvent registeredEvent)
        {
            Console.WriteLine("Custormer notification sent order id" + $"{registeredEvent.OrderId} registerd");
        }
    }
}
