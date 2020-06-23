using System.Windows.Input;
using Foundation;
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

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = base.GetCell(tableView, indexPath);

            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cell;
        }
    }
}
