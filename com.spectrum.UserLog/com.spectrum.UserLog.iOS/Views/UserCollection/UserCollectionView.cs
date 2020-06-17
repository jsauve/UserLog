using System;
using com.spectrum.UserLog.Core;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public partial class UserCollectionView : MvxCollectionViewController
    {
        public UserCollectionView() : base("UserCollectionView", null)
        {
        }

        public UserCollectionView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<UserCollectionView, UserCollectionViewModel>();
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

