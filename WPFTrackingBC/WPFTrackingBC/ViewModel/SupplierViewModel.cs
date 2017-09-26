using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTrackingBC.Models;

namespace WPFTrackingBC.ViewModel
{
    public class SupplierViewModel:ViewModelBase
    {
        private ObservableCollection<ShipmentDetails> _containerList;
        private ObservableCollection<PaymentDetails> _paymentDetails;
        public ObservableCollection<PaymentDetails> PaymentDetails
        {
            get
            {
                return _paymentDetails;
            }
            set
            {
                _paymentDetails = value;
                NotifyPropertyChanged("PaymentDetails");
            }
        }
        public ObservableCollection<ShipmentDetails> ContainerList
        {
            get
            {
                return _containerList;
            }
            set
            {
                _containerList = value;
                NotifyPropertyChanged("ContainerList");
            }
        }
        private string _supplier;
        public string Supplier
        {
            get
            {
                return _supplier;
            }
            set
            {
                _supplier = value;
                NotifyPropertyChanged("Supplier");
            }
        }
        private string _bank;
        public string Bank
        {
            get
            {
                return _bank;
            }
            set
            {
                _bank = value;
                NotifyPropertyChanged("Bank");
            }
        }
        private int _balance;
        public int Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                NotifyPropertyChanged("Balance");
            }
        }
    }
}
