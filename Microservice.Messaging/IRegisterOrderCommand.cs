using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Messaging
{
    public interface IRegisterOrderCommand
    {
        string PickupName { get; set; }
        string PickupAddress { get; set; }
        string PickupCity { get; set; }
        string DeliverName { get; set; }
        string DeliverAddress { get; set; }
        string DeliverCity { get; set; }
    }
}
