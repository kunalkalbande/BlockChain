using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAPI.Models
{
    public class WeighingRequest
    {
        public string Id { get; set; }
        public int Status
        {
            get; set;
        }
        public string ContainerName { get; set; }
        public int Weight { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}