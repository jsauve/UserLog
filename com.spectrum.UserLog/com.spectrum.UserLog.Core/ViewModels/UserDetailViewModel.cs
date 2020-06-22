using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Navigation;

namespace com.spectrum.UserLog.Core
{
    public class UserDetailViewModel : BaseViewModel<User, ModelResult<User>>
    {
        private readonly IMvxNavigationService _NavigationService;
        private readonly IUserDialogs _UserDialogs;

        public User User { get; private set; }

        private ModelAction ModelAction = ModelAction.Error;

        public UserDetailViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
        {
            _NavigationService = navigationService;
            _UserDialogs = userDialogs;
        }

        public override void Prepare(User parameter)
        {
            User = parameter;

            if (User?.Id == null)
                ModelAction = ModelAction.Create;
            else
                ModelAction = ModelAction.Update;
        }

        public async Task Pop()
        {
            await _NavigationService.Close(this);
        }

        public async Task Save()
        {
            var result = new ModelResult<User>(User, ModelAction);

            await _NavigationService.Close(this, result);
        }

        public async Task Delete()
        {
            var result = new ModelResult<User>(User, ModelAction.Delete);

            await _NavigationService.Close(this, result);
        }
    }
}
