using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Microservice.Messaging
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(
            Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
                registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(RabbitMqConstant.RabbitMqUri), hst =>
                {
                    hst.Username(RabbitMqConstant.UserName);
                    hst.Password(RabbitMqConstant.Password);
                });
                registrationAction?.Invoke(cfg,host);
            });
        }
    }
}
