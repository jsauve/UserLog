using System;
using AsyncAwaitBestPractices;
using Cirrious.FluentLayouts.Touch;
using com.spectrum.UserLog.Core;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public class UserDetailView : MvxViewController<UserDetailViewModel>
    {
        const float PADDING = 12f;

        private readonly UIColor LabelTextColor = UIColor.Gray;
        private readonly UIColor FieldTextColor = UIColor.Black;

        private readonly UIFont LabelFont =  UIFont.SystemFontOfSize(14f);
        private readonly UIFont FieldFont = UIFont.SystemFontOfSize(15f);

        public UIScrollView ScrollView { get; private set; }

        public UILabel UsernameLabel { get; private set; }
        public UITextField UsernameField { get; private set; }

        public UILabel FirstNameLabel { get; private set; }
        public UITextField FirstNameField { get; private set; }

        public UILabel LastNameLabel { get; private set; }
        public UITextField LastNameField { get; private set; }

        public UILabel OldPasswordLabel { get; private set; }
        public UITextField OldPasswordField { get; private set; }

        public UILabel NewPasswordLabel { get; private set; }
        public UITextField NewPasswordField { get; private set; }

        public UILabel NewPasswordVerifyLabel { get; private set; }
        public UITextField NewPasswordVerifyField { get; private set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var backButton = new UIButton(UIButtonType.System);
            backButton.SetTitle("Cancel", UIControlState.Normal);
            backButton.TouchUpInside += (sender, e) => ViewModel.Pop().SafeFireAndForget();
            NavigationController.TopViewController.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);

            var saveButton = new UIButton(UIButtonType.System);
            saveButton.SetTitle("Save", UIControlState.Normal);
            saveButton.TouchUpInside += SaveButton_TouchDown;
            NavigationController.TopViewController.NavigationItem.RightBarButtonItem = new UIBarButtonItem(saveButton);

            View.BackgroundColor = UIColor.White;

            ScrollView = new UIScrollView();

            UsernameLabel = new UILabel()
            {
                Text = "Username",
                Font = LabelFont,
                TextColor = LabelTextColor
            };

            FirstNameLabel = new UILabel()
            {
                Text = "First Name",
                Font = LabelFont,
                TextColor = LabelTextColor
            };

            LastNameLabel = new UILabel()
            {
                Text = "Last Name",
                Font = LabelFont,
                TextColor = LabelTextColor
            };

            OldPasswordLabel = new UILabel()
            {
                Text = "Old Password",
                Font = LabelFont,
                TextColor = LabelTextColor
            };

            NewPasswordLabel = new UILabel()
            {
                Text = "New Password",
                Font = LabelFont,
                TextColor = LabelTextColor
            };

            NewPasswordVerifyLabel = new UILabel()
            {
                Text = "Verify New Password",
                Font = LabelFont,
                TextColor = LabelTextColor
            };

            UsernameField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor, 
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            FirstNameField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor,
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            LastNameField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor,
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            OldPasswordField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor,
                BorderStyle = UITextBorderStyle.RoundedRect,
                SecureTextEntry = true
            };

            NewPasswordField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor,
                BorderStyle = UITextBorderStyle.RoundedRect,
                SecureTextEntry = true
            };

            NewPasswordVerifyField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor,
                BorderStyle = UITextBorderStyle.RoundedRect,
                SecureTextEntry = true
            };

            View.AddSubviews(
                UsernameLabel,
                UsernameField,
                FirstNameLabel,
                FirstNameField,
                LastNameLabel,
                LastNameField,
                OldPasswordLabel,
                OldPasswordField,
                NewPasswordLabel,
                NewPasswordField,
                NewPasswordVerifyLabel,
                NewPasswordVerifyField);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                UsernameLabel.AtLeftOf(View, PADDING),
                UsernameLabel.AtTopOfSafeArea(View, PADDING),
                UsernameLabel.AtRightOf(View, PADDING),
                UsernameField.AtLeftOf(View, PADDING),
                UsernameField.AtBottomOf(UsernameLabel, -PADDING * 3),
                UsernameField.AtRightOf(View, PADDING),
                FirstNameLabel.AtLeftOf(View, PADDING),
                FirstNameLabel.AtBottomOf(UsernameField, -PADDING * 3),
                FirstNameLabel.AtRightOf(View, PADDING),
                FirstNameField.AtLeftOf(View, PADDING),
                FirstNameField.AtBottomOf(FirstNameLabel, -PADDING * 3),
                FirstNameField.AtRightOf(View, PADDING),
                LastNameLabel.AtLeftOf(View, PADDING),
                LastNameLabel.AtBottomOf(FirstNameField, -PADDING * 3),
                LastNameLabel.AtRightOf(View, PADDING),
                LastNameField.AtLeftOf(View, PADDING),
                LastNameField.AtBottomOf(LastNameLabel, -PADDING * 3),
                LastNameField.AtRightOf(View, PADDING),
                OldPasswordLabel.AtLeftOf(View, PADDING),
                OldPasswordLabel.AtBottomOf(LastNameField, -PADDING * 3),
                OldPasswordLabel.AtRightOf(View, PADDING),
                OldPasswordField.AtLeftOf(View, PADDING),
                OldPasswordField.AtBottomOf(OldPasswordLabel, -PADDING * 3),
                OldPasswordField.AtRightOf(View, PADDING),
                NewPasswordLabel.AtLeftOf(View, PADDING),
                NewPasswordLabel.AtBottomOf(OldPasswordField, -PADDING * 3),
                NewPasswordLabel.AtRightOf(View, PADDING),
                NewPasswordField.AtLeftOf(View, PADDING),
                NewPasswordField.AtBottomOf(NewPasswordLabel, -PADDING * 3),
                NewPasswordField.AtRightOf(View, PADDING),
                NewPasswordVerifyLabel.AtLeftOf(View, PADDING),
                NewPasswordVerifyLabel.AtBottomOf(NewPasswordField, -PADDING * 3),
                NewPasswordVerifyLabel.AtRightOf(View, PADDING),
                NewPasswordVerifyField.AtLeftOf(View, PADDING),
                NewPasswordVerifyField.AtBottomOf(NewPasswordVerifyLabel, -PADDING * 3),
                NewPasswordVerifyField.AtRightOf(View, PADDING)
                );

            var set = this.CreateBindingSet<UserDetailView, UserDetailViewModel>();
            set.Bind().For(x => x.Title).To(x => x.UserClone.DisplayName);
            set.Bind(UsernameField).For(x => x.Text).To(x => x.UserClone.Username);
            set.Bind(FirstNameField).For(x => x.Text).To(x => x.UserClone.FirstName);
            set.Bind(LastNameField).For(x => x.Text).To(x => x.UserClone.LastName);
            set.Apply();
        }

        private void SaveButton_TouchDown(object sender, EventArgs e)
        {
            // if new password field has a value

            // check to see that old password is valid

            // check if new password is valid

            // check if verify matches new password
            

            ViewModel.Save().SafeFireAndForget();
        }
    }
}
