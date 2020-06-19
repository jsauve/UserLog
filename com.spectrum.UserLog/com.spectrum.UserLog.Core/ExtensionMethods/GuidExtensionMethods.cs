using System;
namespace com.spectrum.UserLog.Core
{
    public static class GuidExtensionMethods
    {
        public static string ToNormalizedString(this Guid guid) => guid.ToString().ToLower();
    }
}
