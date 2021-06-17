using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProjectorProMobile.Pages
{
    public class CapStringLengthConverter : IValueConverter
    {

        private const int MAX_TITLE_LENGTH = 42;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string item = value as string;
            if (!string.IsNullOrEmpty(item) && item.Length > MAX_TITLE_LENGTH)
            {
                return item.Substring(0, MAX_TITLE_LENGTH) + "…";
            }
            else
            {
                return item;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
