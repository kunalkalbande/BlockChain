using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTrackingBC.Models;

namespace WPFTrackingBC.ViewModel
{
    class ShipmentDetailViewModel:ViewModelBase
    {
        private ObservableCollection<ShipmentDetails> _containerList;
        private ObservableCollection<ShipmentDetails> _notificationList;
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
        public ObservableCollection<ShipmentDetails> NotificationList
        {
            get
            {
                return _notificationList;
            }
            set
            {
                _notificationList = value;
                NotifyPropertyChanged("NotificationList");
            }
        }

    }
}
