using System;
using System.Collections.Generic;
using System.Linq;

namespace com.spectrum.UserLog.Core
{
    public class PasswordValidationResult
    {
        public bool IsValid => !Reasons.Any();
        public IList<string> Reasons { get; set; } = new List<string>();
    }
}
