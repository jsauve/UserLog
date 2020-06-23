using System;
using System.Globalization;
using MvvmCross.Converters;

namespace com.spectrum.UserLog.Core
{
    public class GuidValueConverter : MvxValueConverter<Guid, string>
    {
        protected override string Convert(Guid value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == Guid.Empty)
                return "Auto-generated";
            else
                return value.ToNormalizedString();
        }

        protected override Guid ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
