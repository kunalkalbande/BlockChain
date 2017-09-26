using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAPI.Models
{
    public class ApprovalRequest
    {
        [Parameter("int", "docId", 6, false)]
        public int DocId { get; set; }
        [Parameter("address", "containerId", 1, false)]
        public string Id { get; set; }
        [Parameter("string", "document", 2, false)]
        public string Document { get; set; }
        [Parameter("string", "date", 4, false)]
        public string TransactionTime { get; set; }
        private int s;
        [Parameter("int", "_status", 3, false)]
        public int _status
        {
            get;set;
        }
        [Parameter("string", "docurl", 5, false)]
        public string Url { get; set; }
    }
}