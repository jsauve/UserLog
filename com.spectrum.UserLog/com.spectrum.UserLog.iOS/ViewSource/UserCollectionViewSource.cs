using System;
using com.spectrum.UserLog.Core;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public class UserCollectionViewSource : MvxCollectionViewSource
    {
        UserCollectionViewModel UserCollectionViewModel { get; set; }

        public UserCollectionViewSource(UserCollectionViewModel userCollectionViewModel, UICollectionView collectionView, NSString defaultCellIdentifier) : base(collectionView, defaultCellIdentifier)
        {
            UserCollectionViewModel = userCollectionViewModel;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return UserCollectionViewModel.Users.Count;
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
        {
            return collectionView.DequeueReusableCell(DefaultCellIdentifier, indexPath) as UserCollectionViewCell;
        }
    }
}
