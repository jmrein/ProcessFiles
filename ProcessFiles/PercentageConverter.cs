/* PercentageConverter.cs
 *
 * Copyright (C) 2016 Joel Rein
 * All rights reserved.
 *
 * This software may be modified and distributed under the terms
 * of the BSD license.  See the LICENSE file for details.
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace ProcessFiles
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