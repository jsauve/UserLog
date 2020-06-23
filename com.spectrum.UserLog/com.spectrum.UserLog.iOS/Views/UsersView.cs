using Cirrious.FluentLayouts.Touch;
using com.spectrum.UserLog.Core;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public class UsersView : MvxViewController<UsersViewModel>
    {
        private MvxUIRefreshControl _RefreshControl;
        private UITableView _TableView;
        private UIBarButtonItem _NewButton;
        private UsersTableViewSource _Source;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _NewButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            NavigationItem.RightBarButtonItem = _NewButton;

            _RefreshControl = new MvxUIRefreshControl();

            _TableView = new UITableView
            {
                BackgroundColor = UIColor.White,
                RowHeight = UITableView.AutomaticDimension,
                EstimatedRowHeight = 44f
            };
            _TableView.AddSubview(_RefreshControl);

            _Source = new UsersTableViewSource(_TableView);
            _TableView.Source = _Source;

            View.AddSubviews(_TableView);
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _TableView.AtLeftOf(View),
                _TableView.AtTopOf(View),
                _TableView.AtBottomOf(View),
                _TableView.AtRightOf(View)
            );

            View.BringSubviewToFront(_TableView);

            var set = this.CreateBindingSet<UsersView, UsersViewModel>();
            set.Bind(this).For(x => x.Title).To(vm => vm.Title);
            set.Bind(this).For("NetworkIndicator").To(vm => vm.FetchUsersTask.IsNotCompleted).WithFallback(false);
            set.Bind(_RefreshControl).For(r => r.IsRefreshing).To(vm => vm.LoadUsersTask.IsNotCompleted).WithFallback(false);
            set.Bind(_RefreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshUsersCommand);
            set.Bind(_NewButton).To(vm => vm.CreateNewUserCommand);
            set.Bind(_Source).For(v => v.ItemsSource).To(vm => vm.Users);
            set.Bind(_Source).For(v => v.SelectionChangedCommand).To(vm => vm.UserSelectedCommand);
            set.Bind(_Source).For(v => v.FetchCommand).To(vm => vm.FetchUsersCommand);
            set.Apply();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ViewModel.RefreshUsersCommand.Execute();
        }
    }
}
