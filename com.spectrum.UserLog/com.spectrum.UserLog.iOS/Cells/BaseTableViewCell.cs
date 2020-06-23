using System;
using MvvmCross.Platforms.Ios.Binding.Views;

namespace com.spectrum.UserLog.iOS
{
    public abstract class BaseTableViewCell : MvxTableViewCell
    {
        private bool _DidSetupConstraints;

        public BaseTableViewCell(IntPtr handle) : base(handle)
        {
            RunLifecycle();
        }

        private void RunLifecycle()
        {
            CreateView();

            SetNeedsUpdateConstraints();
        }

        public override sealed void UpdateConstraints()
        {
            if (!_DidSetupConstraints)
            {
                CreateConstraints();
                _DidSetupConstraints = true;
            }

            base.UpdateConstraints();
        }

        protected virtual void CreateView()
        {
        }

        protected virtual void CreateConstraints()
        {
        }
    }
}
