using Nethereum.ABI.FunctionEncoding.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WPFTrackingBC.ViewModel;

namespace WPFTrackingBC.Models
{
    public class ApprovalsDetails:ViewModelBase
    {
        [Parameter("int", "docId",6, false)]
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
            get { return s; } set { s = value; NotifyPropertyChanged("_status"); }
        }
        
        public ApprovalStatus Status
        {
            get { return (ApprovalStatus)_status; }
        }
        [Parameter("string", "docurl", 5, false)]

        public string Url { get; set; }

    }
    public enum ApprovalStatus
    {
        [XmlEnum("0")]
        Pending = 0,
        [XmlEnum("1")]
        Approved = 1,
        [XmlEnum("2")]
        Rejected = 2
    }
}
