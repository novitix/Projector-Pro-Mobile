using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProjectorProMobile.Pages
{
    public class TitleScrollConverter : IValueConverter
    {

        double prevScrollY = 0;
        double currentTitleHeight = 50;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double scrollY = (double)value;
            double newTitleHeight = GetNewTitleHeight(scrollY);
            prevScrollY = scrollY;
            currentTitleHeight = newTitleHeight;
            return newTitleHeight;
        }

        private double GetNewTitleHeight(double scrollY)
        {
            double heightChange = prevScrollY - scrollY;
            double newTitleHeight = currentTitleHeight + heightChange;
            if (newTitleHeight > 50) newTitleHeight = 50;
            if (newTitleHeight <= 0) newTitleHeight = 0.01;
            return newTitleHeight;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
