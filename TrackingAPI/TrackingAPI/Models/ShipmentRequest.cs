using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAPI.Models
{
    public class ShipmentRequest
    {
        [Parameter("address", "containerId", 1, false)]
        public string Id { get; set; }
        [Parameter("string", "date", 2, false)]
        public string TransactionTime { get; set; }
        [Parameter("int", "status", 3, false)]
        public int Status
        {
            get; set;
        }
      
        [Parameter("string", "name", 4, false)]
        public string ContainerName { get; set; }
        [Parameter("int", "weight", 7, false)]
        public int Weight { get; set; }
        [Parameter("string", "source", 5, false)]
        public string Source { get; set; }
        [Parameter("string", "destination", 6, false)]
        public string Destination { get; set; }
        public string Desc { get; set; }
    }
}