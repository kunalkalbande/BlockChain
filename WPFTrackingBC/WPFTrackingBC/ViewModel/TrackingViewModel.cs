using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTrackingBC.Models;

namespace WPFTrackingBC.ViewModel
{
    public class TrackingViewModel:ViewModelBase
    {
        private ObservableCollection<ApprovalsDetails> _approvalsdetail;
        public ObservableCollection<ApprovalsDetails> Approvalsdetail
        {
            get
            {
                return _approvalsdetail;
            }
            set
            {
                _approvalsdetail = value;
                NotifyPropertyChanged("Approvalsdetail");
            }
        }
    }
}
