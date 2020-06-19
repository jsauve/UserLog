using System;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public class UsersTableViewCell : BaseTableViewCell
    {
        private const float PADDING = 12f;

        private UILabel _lblName;

        public UsersTableViewCell(IntPtr handle) : base(handle)
        {
        }

        protected override void CreateView()
        {
            base.CreateView();

            SelectionStyle = UITableViewCellSelectionStyle.None;

            _lblName = new UILabel
            {
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Bold)
            };

            BackgroundColor = UIColor.Clear;
            ContentView.AddSubview(_lblName);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.DelayBind(() =>
            {
                this.AddBindings(_lblName, "DisplayName");
            });
        }

        protected override void CreateConstraints()
        {
            base.CreateConstraints();

            ContentView.AddConstraints(
                _lblName.AtLeftOf(ContentView, PADDING),
                _lblName.AtTopOf(ContentView, PADDING),
                _lblName.AtBottomOf(ContentView, PADDING),
                _lblName.AtRightOf(ContentView, PADDING)
            );
        }
    }
}
