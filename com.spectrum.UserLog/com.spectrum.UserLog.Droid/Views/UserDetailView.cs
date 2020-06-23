using System;
using Android.App;
using Android.OS;

namespace com.spectrum.UserLog.Droid
{
    [Activity(Label = "Detail")]
    public class UserDetailView : BaseView
    {
        protected override int LayoutResource => Resource.Layout.FirstView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
        }
    }
}
