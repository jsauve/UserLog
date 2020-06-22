using System;
using System.Windows.Input;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public class UsersTableViewSource : MvxSimpleTableViewSource
    {
        public ICommand FetchCommand { get; set; }

        public UsersTableViewSource(UITableView tableView) : base(tableView, typeof(UsersTableViewCell))
        {
            DeselectAutomatically = true;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, Foundation.NSIndexPath indexPath, object item)
        {
            var cell = base.GetOrCreateCellFor(tableView, indexPath, item);

            return cell;
        }
    }
}
