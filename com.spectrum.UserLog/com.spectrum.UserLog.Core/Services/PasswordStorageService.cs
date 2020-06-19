using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace com.spectrum.UserLog.Core
{
    public class PasswordStorageService : IPasswordStorageService
    {
        public Task Store(Guid userId, string password) => SecureStorage.SetAsync(userId.ToNormalizedString(), password);

        public void Delete(Guid userId) => SecureStorage.Remove(userId.ToNormalizedString());

        public async Task<bool> Match(Guid userId, string password) => await SecureStorage.GetAsync(userId.ToNormalizedString()) == password;
    }
}
