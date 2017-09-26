using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAPI.Models
{
    public class PaymentRequest
    {
        [Parameter("int", "qty", 1, false)]
        public int Quantity { get; set; }

        [Parameter("string", "supplierName", 2, false)]
        public string SupplierName { get; set; }
        [Parameter("address", "containerId", 3, false)]
        public string ContainerId { get; set; }
        [Parameter("string", "bank", 4, false)]
        public string Bank { get; set; }
        [Parameter("int", "unit", 5, false)]
        public int Unit { get; set; }
        [Parameter("string", "time", 7, false)]
        public string TransactionTime
        { get; set; }
        [Parameter("string", "containername", 6, false)]
        public string ContainerName { get; set; }
    }
}