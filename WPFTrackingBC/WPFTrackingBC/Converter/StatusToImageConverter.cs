using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WPFTrackingBC.Models;

namespace WPFTrackingBC.Converter
{
    class StatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageDetail = (ApprovalStatus)value;
            string path;
            switch(imageDetail)
            {
                case ApprovalStatus.Approved:path = @"/Assets/check.png";
                    break;
                case ApprovalStatus.Rejected:path = @"/Assets/S43Qy.png";
                    break;
                default:return null;

            }
            var uri = new Uri(path, UriKind.RelativeOrAbsolute);

            return new BitmapImage(uri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
