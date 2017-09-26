using Nethereum.ABI.FunctionEncoding.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFTrackingBC.ViewModel;

namespace WPFTrackingBC.Models
{
    public class ShipmentDetails:ViewModelBase
    {
        [Parameter("address", "containerId", 1, false)]
        public string Id { get; set; }
        [Parameter("string", "date", 2, false)]
        public string TransactionTime { get; set; }
        private int s;
        [Parameter("int", "status", 3, false)]
        public int _status
        {
            get { return (int)Status; }
            set { s = value; NotifyPropertyChanged("_status"); Status = (Status)value; NotifyPropertyChanged("Status"); }
        }
       
        public Status Status
        {
            get;set;
        }
        [Parameter("string", "name", 4, false)]
        public string ContainerName { get; set; }
        [Parameter("int", "weight", 7, false)]

        public int Weight { get; set; }
        public Visibility IsVisible { get; set; }
        [Parameter("string", "source", 5, false)]
        public string Source { get; set; }
        [Parameter("string", "destination", 6, false)]
        public string Destination { get; set; }
        public string Desc { get; set; }
        public int Unit { get; set; }
        private string _supplier;
        public string Supplier { get { return _supplier; } set { _supplier = value; NotifyPropertyChanged("Supplier"); } }

    }
}
