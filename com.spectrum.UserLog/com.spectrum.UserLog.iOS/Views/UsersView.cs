using System;
using Cirrious.FluentLayouts.Touch;
using com.spectrum.UserLog.Core;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public class UsersView : MvxViewController<UsersViewModel>
    {
        private MvxUIRefreshControl _refreshControl;
        private UITableView _tableView;
        private UIBarButtonItem _newButton;
        private UsersTableViewSource _source;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _newButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            NavigationItem.RightBarButtonItem = _newButton;

            _refreshControl = new MvxUIRefreshControl();

            _tableView = new UITableView();
            _tableView.BackgroundColor = UIColor.White;
            _tableView.RowHeight = UITableView.AutomaticDimension;
            _tableView.EstimatedRowHeight = 44f;
            _tableView.AddSubview(_refreshControl);

            _source = new UsersTableViewSource(_tableView);
            _tableView.Source = _source;

            View.AddSubviews(_tableView);
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _tableView.AtLeftOf(View),
                _tableView.AtTopOf(View),
                _tableView.AtBottomOf(View),
                _tableView.AtRightOf(View)
            );

            View.BringSubviewToFront(_tableView);

            var set = this.CreateBindingSet<UsersView, UsersViewModel>();
            set.Bind(this).For("NetworkIndicator").To(vm => vm.FetchUsersTask.IsNotCompleted).WithFallback(false);
            set.Bind(_refreshControl).For(r => r.IsRefreshing).To(vm => vm.LoadUsersTask.IsNotCompleted).WithFallback(false);
            set.Bind(_refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshUsersCommand);
            set.Bind(_newButton).To(vm => vm.CreateNewUserCommand);


            set.Bind(_source).For(v => v.ItemsSource).To(vm => vm.Users);
            set.Bind(_source).For(v => v.SelectionChangedCommand).To(vm => vm.UserSelectedCommand);
            set.Bind(_source).For(v => v.FetchCommand).To(vm => vm.FetchUsersCommand);

            set.Apply();
        }
    }
}
