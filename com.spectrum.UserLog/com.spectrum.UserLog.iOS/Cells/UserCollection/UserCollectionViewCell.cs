using System;
using com.spectrum.UserLog.Core;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public partial class UserCollectionViewCell : MvxCollectionViewCell
    {
        public static readonly UINib Nib = UINib.FromName($"{nameof(UserCollectionViewCell)}", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString($"{nameof(UserCollectionViewCell)}");
        public static UserCollectionViewCell Create() => (UserCollectionViewCell)Nib.Instantiate(null, null)[0];

        protected UserCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.

            this.DelayBind(() => {
                var set = this.CreateBindingSet<UserCollectionViewCell, UserModel>();
                set.Bind(NameLabel).To(vm => vm.DisplayName);
                set.Apply();
            });
        } 
    }
}
