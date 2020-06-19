using System;
using System.Threading.Tasks;

namespace com.spectrum.UserLog.Core
{
    public class PasswordPatternValidationService : IPasswordPatternValidationService
    {
        public Task<PasswordValidationResult> Validate(string password)
        {
            throw new NotImplementedException();
        }
    }
}
