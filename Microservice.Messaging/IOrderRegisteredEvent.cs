using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Messaging
{
    public interface IOrderRegisteredEvent
    {
        Guid OrderId { get; set; }
    }
}
