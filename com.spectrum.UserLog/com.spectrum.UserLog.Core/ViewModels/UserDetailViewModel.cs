using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Navigation;

namespace com.spectrum.UserLog.Core
{
    public class UserDetailViewModel : BaseViewModel<User>
    {
        private readonly IMvxNavigationService _NavigationService;
        private readonly IUserDialogs _UserDialogs;
        private readonly IModelService<User> _UsersService;
        private readonly IPasswordPatternValidationService _PasswordPatternValidationService;
        private readonly IPasswordStorageService _PasswordStorageService;

        public User UserClone { get; private set; }

        public string NewPassword { get; set; }
        public string NewPasswordVerify { get; set; }

        public bool IsNew { get; private set; }

        public UserDetailViewModel(
            IMvxNavigationService navigationService,
            IUserDialogs userDialogs,
            IModelService<User> usersService,
            IPasswordPatternValidationService passwordPatternValidationService,
            IPasswordStorageService passwordStorageService)
        {
            _NavigationService = navigationService;
            _UserDialogs = userDialogs;
            _UsersService = usersService;
            _PasswordPatternValidationService = passwordPatternValidationService;
            _PasswordStorageService = passwordStorageService;
        }

        public override void Prepare()
        {
            UserClone = new User();
            IsNew = true;
        }

        public override void Prepare(User parameter)
        {
            if (parameter == null || parameter.Id == Guid.Empty)
            {
                UserClone = new User();
                IsNew = true;
            }
            else
            {
                UserClone = parameter.Clone() as User;
                IsNew = false;
            }
        }

        public async Task Cancel() => await _NavigationService.Close(this);

        public async Task Save()
        {
            var reasons = await Validate();

            if (reasons.Any())
            {
                var alertConfig = new AlertConfig()
                {
                    Title = "Oops! Some things to fix...",
                    Message = string.Join(Environment.NewLine, reasons),
                    OkText = "OK"
                };
                await _UserDialogs.AlertAsync(alertConfig);
            }
            else
            {
                if (IsNew)
                {
                    var user = await _UsersService.Create(UserClone);
                    if (!string.IsNullOrWhiteSpace(NewPassword))
                        await _PasswordStorageService.Store(user.Id, NewPassword);
                }
                else
                {
                    var user = await _UsersService.Update(UserClone);
                    if (!string.IsNullOrWhiteSpace(NewPassword))
                        await _PasswordStorageService.Store(user.Id, NewPassword);
                }

                await _NavigationService.Close(this);
            }
        }

        async Task<bool> GetUsernameIsUnique(string username)
        {
            var users = await _UsersService.Read();

            var usernameIsUnique = !users.Any(x => x.Username.ToLower() == username.ToLower());

            return usernameIsUnique;
        }

        public async Task Delete()
        {
            var confirmDelete = await _UserDialogs.ConfirmAsync($"Are you sure you want to delete {UserClone.DisplayName}?", "Delete?", "Delete", "Cancel");

            if (confirmDelete)
            {
                await _UsersService.Delete(UserClone.Id);
                _PasswordStorageService.Delete(UserClone.Id);

                await _NavigationService.Close(this);
            }
        }

        async Task<IList<string>> Validate()
        {
            var reasons = new List<string>();

            var alphaRegex = new Regex("[a-zA-Z]");
            var alphaNumericRegex = new Regex("[a-zA-Z0-9]");

            if (IsNew && !await GetUsernameIsUnique(UserClone.Username))
                reasons.Add($"• {nameof(UserClone.Username)}: must be unique");

            if (string.IsNullOrWhiteSpace(UserClone.Username) || UserClone.Username.Length < 5 || UserClone.Username.Length > 12 || !alphaNumericRegex.IsMatch(UserClone.Username))
                reasons.Add($"• {nameof(UserClone.Username)}: 5-12 length, alphanumeric");

            if (string.IsNullOrWhiteSpace(UserClone.FirstName) || UserClone.FirstName.Length < 1 || UserClone.FirstName.Length > 32 || !alphaRegex.IsMatch(UserClone.FirstName))
                reasons.Add($"• {nameof(UserClone.FirstName)}: 1-32 length, alpha");

            if (string.IsNullOrWhiteSpace(UserClone.LastName) || UserClone.LastName.Length < 1 || UserClone.LastName.Length > 32 || !alphaRegex.IsMatch(UserClone.LastName))
                reasons.Add($"• {nameof(UserClone.LastName)}: 1-32 length, alpha");

            if (IsNew || (!string.IsNullOrWhiteSpace(NewPassword) || !string.IsNullOrWhiteSpace(NewPasswordVerify)))
            {
                var pwValidationResult = _PasswordPatternValidationService.Validate(NewPassword);

                foreach (var pwr in pwValidationResult.Reasons)
                    reasons.Add($"• New password: {pwr}");

                if (pwValidationResult.IsValid && NewPassword != NewPasswordVerify)
                    reasons.Add($"• Passwords must match");
            }

            return reasons;
        }
    }
}
