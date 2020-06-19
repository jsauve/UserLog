using System;
using Acr.UserDialogs;
using MvvmCross.Navigation;

namespace com.spectrum.UserLog.Core
{
    public class UserDetailViewModel : BaseViewModel<User, ModelResult<User>>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;

        public User User { get; private set; }

        public UserDetailViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
        }

        public override void Prepare(User parameter)
        {
            User = parameter;
        }
    }
}
