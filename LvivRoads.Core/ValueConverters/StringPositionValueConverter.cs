using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using LvivRoads.Core.Services;

namespace LvivRoads.Core.ValueConverters
{
    public class StringPositionValueConverter : MvxValueConverter<string, Position>
    {
        protected override Position Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Position(value);
        }

        protected override string ConvertBack(Position value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}