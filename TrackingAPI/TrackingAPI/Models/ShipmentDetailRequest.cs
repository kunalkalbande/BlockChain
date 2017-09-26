using Nethereum.ABI.FunctionEncoding.Attributes;

namespace TrackingAPI.Models
{
    public class ShipmentDetailRequest
    {
        [Parameter("int", "unit", 2, false)]
        public int Quantity { get; set; }

        [Parameter("string", "supplier", 3, false)]
        public string SupplierName { get; set; }
        [Parameter("address", "containerId", 1, false)]
        public string ContainerId { get; set; }
        [Parameter("string", "containerName", 4, false)]
        public string ContainerName { get; set; }
    }
}