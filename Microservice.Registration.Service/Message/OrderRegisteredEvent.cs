using System;
using System.Collections.Generic;
using System.Text;
using Microservice.Messaging;

namespace Microservice.Registration.Service.Message
{
    public class OrderRegisteredEvent:IOrderRegisteredEvent
    {
        private IRegisterOrderCommand command;
        private int id;

        public OrderRegisteredEvent(IRegisterOrderCommand command, int id)
        {
            this.command = command;
            this.id = id;
        }

        public Guid OrderId { get; set; }
    }
}
