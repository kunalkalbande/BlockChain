using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WPFTrackingBC.Models;

namespace WPFTrackingBC.Converter
{
    class ApprovalStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status status = (Status)value;
            int p = System.Convert.ToInt32(parameter);
            switch (p)
            {
                case 1:
                    switch (status)
                    {
                        case Status.PendingSupplierApproval:
                        case Status.Initializing:
                        case Status.Initiated: return new SolidColorBrush(Colors.Black);
                        case Status.ExciseRejected: return new SolidColorBrush(Colors.Red);
                        default: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    }
                case 2:
                    switch (status)
                    {
                        case Status.PendingSupplierApproval:
                        case Status.ExciseRejected:
                        case Status.Initializing:
                        case Status.Initiated:
                        case Status.ExciseApproved:
                            return new SolidColorBrush(Colors.Black);
                        case Status.WeighingRejected: return new SolidColorBrush(Colors.Red);
                        default:
                            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    }
                case 4:
                    switch (status)
                    {
                        case Status.PendingSupplierApproval:
                        case Status.Initializing:
                        case Status.Initiated: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                        case Status.ExciseRejected: return new SolidColorBrush(Colors.Gray);
                        default: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#69C6F1"));
                    }
                case 5:
                    switch (status)
                    {
                        case Status.ExciseRejected:
                        case Status.PendingSupplierApproval:
                        case Status.Initializing:
                        case Status.Initiated:
                        case Status.ExciseApproved:
                            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                        case Status.WeighingRejected: return new SolidColorBrush(Colors.Gray);
                        default:
                            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#69C6F1"));
                    }
                case 6:
                    switch (status)
                    {
                        case Status.Initializing:
                        case Status.PendingSupplierApproval:
                        case Status.Initiated:
                        case Status.ExciseApproved:
                        case Status.WeighingApproved:
                        case Status.WeighingRejected:
                        case Status.ExciseRejected:
                        case Status.CustomApproved:
                            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                        case Status.CustomRejected: return new SolidColorBrush(Colors.Red);
                        default: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#69C6F1"));
                    }
                default:
                    switch (status)
                    {
                        case Status.Initializing:
                        case Status.PendingSupplierApproval:
                        case Status.Initiated:
                        case Status.ExciseApproved:
                        case Status.WeighingApproved:
                        case Status.WeighingRejected:
                        case Status.ExciseRejected:
                        case Status.CustomApproved:
                            return new SolidColorBrush(Colors.Black);
                        case Status.CustomRejected: return new SolidColorBrush(Colors.Red);
                        default: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           throw new NotImplementedException();
        }
    }

    class ApprovalStatusToBrColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ApprovalStatus status = (ApprovalStatus)value;
            switch (status)
            {
                case ApprovalStatus.Approved: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#69C6F1"));
                case ApprovalStatus.Rejected: return new SolidColorBrush(Colors.LightGray);
                default: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
