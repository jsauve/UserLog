using Foundation;
using UIKit;
using MvvmCross.Platforms.Ios.Core;
using com.spectrum.UserLog.Core;

namespace com.spectrum.UserLog.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}
