using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace com.spectrum.UserLog.Core
{
    public class UsersViewModel : BaseViewModel
    {
        public string Title { get; set; } = "Users";

        private readonly IModelService<User> _UsersService;

        private readonly IMvxNavigationService _navigationService;

        private readonly IPasswordStorageService _passwordStorageService;

        public MvxObservableCollection<User> Users { get; set; } = new MvxObservableCollection<User>();

        public MvxNotifyTask LoadUsersTask { get; private set; }

        public MvxNotifyTask FetchUsersTask { get; private set; }

        public IMvxCommand<User> UserSelectedCommand { get; private set; }

        public IMvxCommand CreateNewUserCommand { get; private set; }

        public IMvxCommand FetchUsersCommand { get; private set; }

        public IMvxCommand RefreshUsersCommand { get; private set; }

        public UsersViewModel(IModelService<User> usersService, IMvxNavigationService navigationService, IPasswordStorageService passwordStorageService)
        {
            _UsersService = usersService;
            _navigationService = navigationService;
            _passwordStorageService = passwordStorageService;

            UserSelectedCommand = new MvxAsyncCommand<User>(UserSelected);

            CreateNewUserCommand = new MvxAsyncCommand(CreateNewUser);

            FetchUsersCommand = new MvxCommand(() =>
            {
                FetchUsersTask = MvxNotifyTask.Create(LoadUsers);
                RaisePropertyChanged(() => FetchUsersTask);
            });

            RefreshUsersCommand = new MvxCommand(RefreshPeople);
        }

        public override async Task Initialize()
        {
            LoadUsersTask = MvxNotifyTask.Create(LoadUsers);

            await base.Initialize();
        }

        private async Task LoadUsers()
        {
            Users.Clear();
            var users = await _UsersService.Read();
            Users.AddRange(users);
        }

        private async Task UserSelected(User user)
        {
            var result = await _navigationService.Navigate<UserDetailViewModel, User, ModelResult<User>>(user);

            await HandleModelResult(result);
        }

        private async Task CreateNewUser()
        {
            var result = await _navigationService.Navigate<UserDetailViewModel, User, ModelResult<User>>(null);

            await HandleModelResult(result);
        }

        private async Task HandleModelResult(ModelResult<User> result)
        {
            if (result != null)
            {
                switch (result.ModelAction)
                {
                    case ModelAction.Create:
                        var createdUser = await _UsersService.Create(result.Model);
                        await _passwordStorageService.Store(createdUser.Id, createdUser.Password);
                        break;
                    case ModelAction.Update:
                        var updatedUser = await _UsersService.Update(result.Model);
                        if (!string.IsNullOrWhiteSpace(result.Model.Password))
                            await _passwordStorageService.Store(updatedUser.Id, result.Model.Password);
                        break;
                    case ModelAction.Delete:
                        await _UsersService.Delete(result.Model.Id);
                        _passwordStorageService.Delete(result.Model.Id);
                        break;
                }
            }
        }

        private void RefreshPeople()
        {
            LoadUsersTask = MvxNotifyTask.Create(LoadUsers);
            RaisePropertyChanged(() => LoadUsersTask);
        }
    }
}
