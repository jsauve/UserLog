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

        public User UserClone { get; private set; }

        private ModelAction ModelAction = ModelAction.Error;

        public UserDetailViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
        {
            _NavigationService = navigationService;
            _UserDialogs = userDialogs;
        }

        public override void Prepare(User parameter)
        {
            if (parameter == null)
                UserClone = new User();
            else
            {
                UserClone = new User
                {
                    Id = parameter.Id,
                    Username = parameter.Username,
                    FirstName = parameter.FirstName,
                    LastName = parameter.LastName,
                    Password = parameter.Password,
                    CreatedAt = parameter.CreatedAt,
                    UpdatedAt = parameter.UpdatedAt
                };
            }

            if (UserClone?.Id == null)
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
            var result = new ModelResult<User>(UserClone, ModelAction);

            await _NavigationService.Close(this, result);
        }

        public async Task Delete()
        {
            var result = new ModelResult<User>(UserClone, ModelAction.Delete);

            await _NavigationService.Close(this, result);
        }
    }
}
