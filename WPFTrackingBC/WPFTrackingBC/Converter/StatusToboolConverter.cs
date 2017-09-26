using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFTrackingBC.Models;

namespace WPFTrackingBC.Converter
{
    class StatusToboolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status status = (Status)value;
            switch(App.UserType)
            {
                case UserType.Initiater:return true;
                case UserType.Custom:switch(status)
                    {
                        case Status.Initiated:return true;
                        default: return false;
                    }
                case UserType.Weighing:
                    switch (status)
                    {
                        case Status.CustomApproved: return true;
                        default: return false;
                    }
                    
                default:return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
