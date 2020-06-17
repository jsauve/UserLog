using System;
using MvvmCross.ViewModels;
using PropertyChanged;

namespace com.spectrum.UserLog.Core
{
    public class UserCollectionViewModel : BaseCollectionViewModel<UserModel>
    {
        public MvxObservableCollection<UserModel> Users { get; private set; } = new MvxObservableCollection<UserModel>();

        public UserCollectionViewModel()
        {
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();


        }
    }
}
