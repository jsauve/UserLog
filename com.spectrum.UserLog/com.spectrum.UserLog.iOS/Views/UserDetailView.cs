using System;
using AsyncAwaitBestPractices;
using Cirrious.FluentLayouts.Touch;
using com.spectrum.UserLog.Core;
using CoreGraphics;
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

        public UIView ScrollContent { get; private set; }

        public UILabel UsernameLabel { get; private set; }
        public UITextField UsernameField { get; private set; }

        public UILabel IdLabel { get; private set; }
        public UITextField IdField { get; private set; }

        public UILabel FirstNameLabel { get; private set; }
        public UITextField FirstNameField { get; private set; }

        public UILabel LastNameLabel { get; private set; }
        public UITextField LastNameField { get; private set; }

        public UILabel NewPasswordLabel { get; private set; }
        public UITextField NewPasswordField { get; private set; }

        public UILabel NewPasswordVerifyLabel { get; private set; }
        public UITextField NewPasswordVerifyField { get; private set; }

        public UIButton DeleteButton { get; private set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var backButton = new UIButton(UIButtonType.System);
            backButton.SetTitle("Cancel", UIControlState.Normal);
            backButton.TouchUpInside += (sender, e) => ViewModel.Cancel().SafeFireAndForget();
            NavigationController.TopViewController.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);

            var saveButton = new UIButton(UIButtonType.System);
            saveButton.SetTitle("Save", UIControlState.Normal);
            saveButton.TouchUpInside += SaveButton_TouchUpInside;
            NavigationController.TopViewController.NavigationItem.RightBarButtonItem = new UIBarButtonItem(saveButton);

            DeleteButton = new UIButton(UIButtonType.System);
            DeleteButton.SetTitle("Delete", UIControlState.Normal);
            DeleteButton.TouchUpInside += DeleteButton_TouchUpInside;

            View.BackgroundColor = UIColor.White;

            ScrollView = new UIScrollView();

            ScrollContent = new UIView();

            UsernameLabel = new UILabel()
            {
                Text = "Username",
                Font = LabelFont,
                TextColor = LabelTextColor
            };

            IdLabel = new UILabel()
            {
                Text = "Id (cannot edit)",
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
                KeyboardType = UIKeyboardType.NamePhonePad,
                AutocapitalizationType = UITextAutocapitalizationType.None,
                AutocorrectionType = UITextAutocorrectionType.No,
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            IdField = new UITextField()
            {
                Enabled = false,
                Font = FieldFont,
                TextColor = FieldTextColor,
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            FirstNameField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor,
                TextContentType = UITextContentType.GivenName,
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            LastNameField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor,
                TextContentType = UITextContentType.FamilyName,
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            NewPasswordField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor,
                AutocapitalizationType = UITextAutocapitalizationType.None,
                TextContentType = UITextContentType.Password,
                BorderStyle = UITextBorderStyle.RoundedRect,
                SecureTextEntry = true,
                Placeholder = "5-12 long, letters & numbers, no repeats"
            };

            NewPasswordVerifyField = new UITextField()
            {
                Font = FieldFont,
                TextColor = FieldTextColor,
                AutocapitalizationType = UITextAutocapitalizationType.None,
                TextContentType = UITextContentType.Password,
                BorderStyle = UITextBorderStyle.RoundedRect,
                SecureTextEntry = true
            };

            ScrollContent.AddSubviews(
                UsernameLabel,
                UsernameField,
                IdLabel,
                IdField,
                FirstNameLabel,
                FirstNameField,
                LastNameLabel,
                LastNameField,
                NewPasswordLabel,
                NewPasswordField,
                NewPasswordVerifyLabel,
                NewPasswordVerifyField,
                DeleteButton);
            ScrollContent.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            ScrollContent.AddConstraints(
                UsernameLabel.AtLeftOf(ScrollContent, PADDING),
                UsernameLabel.AtTopOfSafeArea(ScrollContent, PADDING),
                UsernameLabel.AtRightOf(ScrollContent, PADDING),
                UsernameField.AtLeftOf(ScrollContent, PADDING),
                UsernameField.AtBottomOf(UsernameLabel, -PADDING * 3),
                UsernameField.AtRightOf(ScrollContent, PADDING),
                IdLabel.AtLeftOf(ScrollContent, PADDING),
                IdLabel.AtBottomOf(UsernameField, -PADDING * 3),
                IdLabel.AtRightOf(ScrollContent, PADDING),
                IdField.AtLeftOf(ScrollContent, PADDING),
                IdField.AtBottomOf(IdLabel, -PADDING * 3),
                IdField.AtRightOf(ScrollContent, PADDING),
                FirstNameLabel.AtLeftOf(ScrollContent, PADDING),
                FirstNameLabel.AtBottomOf(IdField, -PADDING * 3),
                FirstNameLabel.AtRightOf(ScrollContent, PADDING),
                FirstNameField.AtLeftOf(ScrollContent, PADDING),
                FirstNameField.AtBottomOf(FirstNameLabel, -PADDING * 3),
                FirstNameField.AtRightOf(ScrollContent, PADDING),
                LastNameLabel.AtLeftOf(ScrollContent, PADDING),
                LastNameLabel.AtBottomOf(FirstNameField, -PADDING * 3),
                LastNameLabel.AtRightOf(ScrollContent, PADDING),
                LastNameField.AtLeftOf(ScrollContent, PADDING),
                LastNameField.AtBottomOf(LastNameLabel, -PADDING * 3),
                LastNameField.AtRightOf(ScrollContent, PADDING),
                NewPasswordLabel.AtLeftOf(ScrollContent, PADDING),
                NewPasswordLabel.AtBottomOf(LastNameField, -PADDING * 3),
                NewPasswordLabel.AtRightOf(ScrollContent, PADDING),
                NewPasswordField.AtLeftOf(ScrollContent, PADDING),
                NewPasswordField.AtBottomOf(NewPasswordLabel, -PADDING * 3),
                NewPasswordField.AtRightOf(ScrollContent, PADDING),
                NewPasswordVerifyLabel.AtLeftOf(ScrollContent, PADDING),
                NewPasswordVerifyLabel.AtBottomOf(NewPasswordField, -PADDING * 3),
                NewPasswordVerifyLabel.AtRightOf(ScrollContent, PADDING),
                NewPasswordVerifyField.AtLeftOf(ScrollContent, PADDING),
                NewPasswordVerifyField.AtBottomOf(NewPasswordVerifyLabel, -PADDING * 3),
                NewPasswordVerifyField.AtRightOf(ScrollContent, PADDING),
                DeleteButton.AtLeftOf(ScrollContent, PADDING),
                DeleteButton.AtBottomOf(NewPasswordVerifyField, -PADDING * 3),
                DeleteButton.AtRightOf(ScrollContent, PADDING));

            ScrollView.AddSubview(ScrollContent);
            ScrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            ScrollView.AddConstraints(
                ScrollContent.WithSameWidth(ScrollView),
                ScrollContent.WithSameHeight(ScrollView));

            View.AddSubview(ScrollView);
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(
                ScrollContent.WithSameWidth(View),
                ScrollContent.WithSameHeight(View));

            var set = this.CreateBindingSet<UserDetailView, UserDetailViewModel>();
            set.Bind().For(x => x.Title).To(x => x.UserClone.DisplayName);
            set.Bind(UsernameField).For(x => x.Text).To(x => x.UserClone.Username);
            set.Bind(IdField).For(x => x.Text).To(x => x.UserClone.Id).Mode(MvvmCross.Binding.MvxBindingMode.OneWay).WithConversion<GuidValueConverter>();
            set.Bind(FirstNameField).For(x => x.Text).To(x => x.UserClone.FirstName);
            set.Bind(LastNameField).For(x => x.Text).To(x => x.UserClone.LastName);
            set.Bind(NewPasswordField).For(x => x.Text).To(x => x.NewPassword);
            set.Bind(NewPasswordVerifyField).For(x => x.Text).To(x => x.NewPasswordVerify);
            set.Bind(DeleteButton).For(x => x.Hidden).To(x => x.IsNew);
            set.Apply();
        }

        private void DeleteButton_TouchUpInside(object sender, EventArgs e)
        {
            ViewModel.Delete().SafeFireAndForget();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            var height = ScrollContent.Bounds.Size.Height + PADDING + ((NavigationController?.NavigationBar?.Bounds == null) ? 0 : NavigationController.NavigationBar.Bounds.Height);

            var size = new CGSize(ScrollContent.Bounds.Size.Width, height);

            ScrollView.ContentSize = size;
        }

        private void SaveButton_TouchUpInside(object sender, EventArgs e)
        {
            ViewModel.Save().SafeFireAndForget();
        }
    }
}
