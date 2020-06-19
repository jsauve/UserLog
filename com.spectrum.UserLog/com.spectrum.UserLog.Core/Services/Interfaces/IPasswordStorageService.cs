using System;
using System.Threading.Tasks;

namespace com.spectrum.UserLog.Core
{
    public interface IPasswordStorageService
    {
        Task Store(Guid userId, string password);
        void Delete(Guid userId);
        Task<bool> Match(Guid userId, string password);
    }
}
