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

        private readonly IModelService<UserModel> _UsersService;

        private readonly IMvxNavigationService _navigationService;

        public MvxObservableCollection<UserModel> Users { get; set; } = new MvxObservableCollection<UserModel>();

        public MvxNotifyTask LoadUsersTask { get; private set; }

        public MvxNotifyTask FetchUsersTask { get; private set; }

        public IMvxCommand<UserModel> UserSelectedCommand { get; private set; }

        public IMvxCommand CreateNewUserCommand { get; private set; }

        public IMvxCommand FetchUsersCommand { get; private set; }

        public IMvxCommand RefreshUsersCommand { get; private set; }

        public UsersViewModel(
            IModelService<UserModel> usersService,
            IMvxNavigationService navigationService)
        {
            _UsersService = usersService;
            _navigationService = navigationService;

            UserSelectedCommand = new MvxAsyncCommand<UserModel>(UserSelected);

            CreateNewUserCommand = new MvxAsyncCommand(CreateNewUser);

            FetchUsersCommand = new MvxCommand(() =>
            {
                FetchUsersTask = MvxNotifyTask.Create(LoadUsers);
                RaisePropertyChanged(() => FetchUsersTask);
            });

            RefreshUsersCommand = new MvxCommand(RefreshUsers);
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

        private async Task UserSelected(UserModel user)
        {
            await _navigationService.Navigate<UserDetailViewModel, UserModel>(user);
        }

        private async Task CreateNewUser()
        {
            await _navigationService.Navigate<UserDetailViewModel>();
        }

        private void RefreshUsers()
        {
            LoadUsersTask = MvxNotifyTask.Create(LoadUsers);
            RaisePropertyChanged(() => LoadUsersTask);
        }
    }
}
