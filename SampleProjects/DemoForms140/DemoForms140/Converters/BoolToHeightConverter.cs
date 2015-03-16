using System;
using Xamarin.Forms;

namespace DemoForms140.Converters
{
	public class BoolToHeightConverter : IValueConverter
	{
		double _height;

		public BoolToHeightConverter(double height = 0)
		{
			_height = height;
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool boolVal = false;
			if(value is bool)
			{
				boolVal = (bool)value; 
			}
			double height = _height;

			return boolVal ? height : (double)0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

