using System;
using System.Threading.Tasks;

namespace com.spectrum.UserLog.Core
{
    public interface IPasswordPatternValidationService
    {
        Task<PasswordValidationResult> Validate(string password);
    }
}
