using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace com.spectrum.UserLog.Core
{
    public class UsersService : IModelService<User>
    {
        private string _FilePath => Path.Combine(FileSystem.AppDataDirectory, "users.json");

        public async Task<User> Create(User user)
        {
            if (user.Id != null)
                throw new ArgumentException($"A new {nameof(User)} must have a null Id. An Id will be created automatically.");

            user.Id = Guid.NewGuid();

            var users = await Read();

            users.Add(user);

            await File.WriteAllTextAsync(_FilePath, JsonConvert.SerializeObject(users));

            return user;
        }

        public async Task<IList<User>> Read()
        {
            await EnsureFileExists();

            var json = File.ReadAllText(_FilePath);

            var users = JsonConvert.DeserializeObject<IList<User>>(json);

            return users;
        }

        public async Task<User> Read(Guid id)
        {
            var users = await Read();

            return users.FirstOrDefault(x => x.Id == id);
        }

        public async Task<User> Update(User user)
        {
            await Delete(user.Id);

            var users = await Read();

            users.Add(user);

            await File.WriteAllTextAsync(_FilePath, JsonConvert.SerializeObject(users));

            return user;
        }

        public async Task Delete(Guid id)
        {
            var users = await Read();

            var existing = users.ToList().Find(x => x.Id == id);

            if (existing == null)
                throw new ArgumentException($"No existing {nameof(User)} found with Id {id}");

            users.Remove(existing);

            await File.WriteAllTextAsync(_FilePath, JsonConvert.SerializeObject(users));
        }

        private async Task EnsureFileExists()
        {
            if (!File.Exists(_FilePath))
                await File.WriteAllTextAsync(_FilePath, "[]");
        }
    }
}
