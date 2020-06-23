using System;
using Cirrious.FluentLayouts.Touch;
using com.spectrum.UserLog.Core;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public class UsersTableViewCell : BaseTableViewCell
    {
        private const float PADDING = 12f;

        public UILabel NameLabel { get; private set; }
        public UILabel UsernameLabel { get; private set; }

        public UsersTableViewCell(IntPtr handle) : base(handle)
        {
        }

        protected override void CreateView()
        {
            base.CreateView();

            SelectionStyle = UITableViewCellSelectionStyle.None;

            NameLabel = new UILabel
            {
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Bold)
            };

            UsernameLabel = new UILabel
            {
                TextColor = UIColor.Gray,
                Font = UIFont.SystemFontOfSize(14f)
            };

            BackgroundColor = UIColor.Clear;
            ContentView.AddSubviews(
                UsernameLabel,
                NameLabel);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var set = this.CreateBindingSet<UsersTableViewCell, UserModel>();
            set.Bind(NameLabel).For(x => x.Text).To(x => x.DisplayName);
            set.Bind(UsernameLabel).For(x => x.Text).To(x => x.Username);
            set.Apply();
        }

        protected override void CreateConstraints()
        {
            base.CreateConstraints();

            ContentView.AddConstraints(
                NameLabel.AtLeftOf(ContentView, PADDING),
                NameLabel.AtTopOf(ContentView, PADDING),
                NameLabel.AtRightOf(ContentView, PADDING),
                UsernameLabel.AtLeftOf(NameLabel, 0),
                UsernameLabel.AtBottomOf(NameLabel, -PADDING-(PADDING/2)),
                UsernameLabel.AtBottomOf(ContentView, PADDING),
                UsernameLabel.AtRightOf(NameLabel, 0)
            );
        }
    }
}
