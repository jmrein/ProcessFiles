using System;
using System.Globalization;
using System.Windows.Data;

namespace FileIO
{
	public class PercentageConverter : IValueConverter
	{
		public MidpointRounding MidpointRounding { get; set; } = MidpointRounding.AwayFromZero;
		public int Decimals { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//var v = (value as float?) ?? 0;
			var v = 0D;
			if (value is double)
			{
				v = (double) value;
			}
			if (value is float)
			{
				v = (float)value;
			}
			return (int) Math.Round(v * 100, Decimals, MidpointRounding);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value as double?) ?? 0 / 100D;
		}
	}
}