using System;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public partial class UserDetailView : MvxViewController
    {
        public UserDetailView() : base("UserDetailView", null)
        {
        }

        public UserDetailView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

