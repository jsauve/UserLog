using System;
using System.Threading.Tasks;

namespace com.spectrum.UserLog.Core
{
    public interface IPasswordPatternValidationService
    {
        PasswordValidationResult Validate(string password);
    }
}
