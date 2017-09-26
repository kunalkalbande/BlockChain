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
    class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status status = (Status)value;
            switch(status)
            {

                case Status.ExciseRejected:
                case Status.CustomRejected:
                case Status.Rejected:
                case Status.WeighingRejected : return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A55"));
                case Status.GatedIn:return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));    
                default:return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD42A"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
