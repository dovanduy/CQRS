﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microservice.Messaging;
using Microservice.Registration.Service.Message;

namespace Microservice.Registration.Service
{
    public class RegisterOrderConsumer:IConsumer<IRegisterOrderCommand>
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

        public async Task Consume(ConsumeContext<IRegisterOrderCommand> context)
        {
            //store order registration in db
            var command = context.Message;
            var id = 12;

            Console.WriteLine($"Order with id {id} registered");
            //notify suscribers
            var orderRegisteredEvent = new OrderRegisteredEvent(command, id);
            //publish event
            await context.Publish<IOrderRegisteredEvent>(orderRegisteredEvent);
        }
    }
}
