using Foundation;
using UIKit;
using MvvmCross.Platforms.Ios.Core;
using com.spectrum.UserLog.Core;

namespace com.spectrum.UserLog.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    
    //public override UIWindow Window
    //{
    //    get;
    //    set;
    //}

    //public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    //{
    //    Window = new UIWindow(UIScreen.MainScreen.Bounds);

    //    var setup = new Setup(this, Window);
    //    setup.Initialize();

    //    var startup = Mvx.Resolve<IMvxAppStart>();
    //    startup.Start();

    //    Window.MakeKeyAndVisible();

    //    return true;
    //}
}
}
