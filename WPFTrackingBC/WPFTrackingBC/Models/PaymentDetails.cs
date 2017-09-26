using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTrackingBC.ViewModel;

namespace WPFTrackingBC.Models
{
    public class PaymentDetails : ViewModelBase
    {
        int amount;
        [Parameter("int", "qty", 1, false)]
        public int Quantity { get { return amount; } set { amount = value;NotifyPropertyChanged("Quantity"); } }

        [Parameter("string", "supplierName", 2, false)]
        public string SupplierName { get; set; }
        [Parameter("address", "containerId", 3, false)]
        public string ContainerId { get; set; }
        [Parameter("string", "bank", 4, false)]
        public string Bank { get; set; }
        [Parameter("int", "unit", 5, false)]
        public int Unit { get; set; }
        private string t;
        [Parameter("string", "time",7, false)]
        public string TransactionTime
        { get { return t; } set { t = value; NotifyPropertyChanged("TransactionTime"); } }
        private Status s;
      
        public Status Status
        {
            get { return s; } set { s = value; NotifyPropertyChanged("Status"); }
        }


        [Parameter("string", "containername", 6, false)]
        public string ContainerName { get; set; }
    }
}
