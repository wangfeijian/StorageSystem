using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace StorageSystem.Common.Converters
{
    public class IntToBrushConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && int.TryParse(value.ToString(), out int result))
            {
                switch (result)
                {
                    case 0:
                        return new SolidColorBrush(Color.FromRgb(192, 192, 192));
                    case 1:
                        return new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    case 2:
                        return new SolidColorBrush(Color.FromRgb(255, 0, 0));

                }
            }
            return new SolidColorBrush(Color.FromRgb(255, 255, 0)); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
