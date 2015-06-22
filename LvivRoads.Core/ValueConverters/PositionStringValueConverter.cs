using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using LvivRoads.Core.Services;

namespace LvivRoads.Core.ValueConverters
{
    public class PositionStringValueConverter : MvxValueConverter<Position, string>
    {
        protected override string Convert(Position value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
        protected override Position ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Position(value);
        }
    }
}