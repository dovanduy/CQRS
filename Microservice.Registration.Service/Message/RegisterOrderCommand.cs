using System;
using System.Collections.Generic;
using System.Text;
using Microservice.Messaging;

namespace Microservice.Registration.Service.Message
{
    public class RegisterOrderCommand: IRegisterOrderCommand
    {
        public string PickupName { get; set; }
        public string PickupAddress { get; set; }
        public string PickupCity { get; set; }
        public string DeliverName { get; set; }
        public string DeliverAddress { get; set; }
        public string DeliverCity { get; set; }
    }
}
