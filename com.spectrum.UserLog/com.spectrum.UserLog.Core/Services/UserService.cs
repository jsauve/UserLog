using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace com.spectrum.UserLog.Core
{
    public class UsersService : IModelService<UserModel>
    {
        private string _FilePath => Path.Combine(FileSystem.AppDataDirectory, $"users.json");

        public async Task<UserModel> Create(UserModel user)
        {
            if (user == null)
                throw new ArgumentException($"{nameof(UserModel)} may not be null.");

            if (!user.Id.Equals(Guid.Empty))
                throw new ArgumentException($"A new {nameof(UserModel)} must have an empty Guid Id.");

            var users = await Read();

            user.Id = Guid.NewGuid();

            users.Add(user);

            await File.WriteAllTextAsync(_FilePath, JsonConvert.SerializeObject(users));

            return user;
        }

        public async Task<IList<UserModel>> Read()
        {
            await EnsureFileExists();

            var json = File.ReadAllText(_FilePath);

            var users = JsonConvert.DeserializeObject<IList<UserModel>>(json);

            return users.OrderBy(x => x.LastName).ToList();
        }

        public async Task<UserModel> Read(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} may not be empty");

            var users = await Read();

            return users.FirstOrDefault(x => x.Id == id);
        }

        public async Task<UserModel> Update(UserModel user)
        {
            if (user == null)
                throw new ArgumentException($"{nameof(UserModel)} may not be null.");

            if (user.Id == null || user.Id == Guid.Empty)
                throw new ArgumentException($"{nameof(UserModel)} may not have a null or empty Id.");

            await Delete(user.Id);

            var users = await Read();

            users.Add(user);

            await File.WriteAllTextAsync(_FilePath, JsonConvert.SerializeObject(users));

            return user;
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} may not be empty");

            var users = await Read();

            var existing = users.ToList().Find(x => x.Id == id);

            if (existing == null)
                throw new ArgumentException($"No existing {nameof(UserModel)} found with Id {id}");

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
