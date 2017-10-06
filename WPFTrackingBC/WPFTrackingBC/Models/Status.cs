using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WPFTrackingBC.Models
{
    public enum Status
    {
        Initiated = 0,
        CustomApproved = 3,
        WeighingApproved=2,
        ExciseApproved=1,
        GatedIn=4,
        CustomRejected=7,
        WeighingRejected=6,
        ExciseRejected=5,
        Rejected=8,
        Initializing=9,
        PaymentApproved=10,
        PendingSupplierApproval=11,
        PortCustomApproved=12,
        Shipped=13
    }

    public enum UserType
    {
        Initiater=0,Custom=3,VGM=2,Excise=1,ExportAuthority=4,Supplier=5
    }

    
}
