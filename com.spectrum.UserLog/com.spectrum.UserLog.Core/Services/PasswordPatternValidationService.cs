using System.Text.RegularExpressions;

namespace com.spectrum.UserLog.Core
{
    public class PasswordPatternValidationService : IPasswordPatternValidationService
    {
        public PasswordValidationResult Validate(string password)
        {
            var result = new PasswordValidationResult();

            if (password.Length < 5 || password.Length > 12)
                result.Reasons.Add(Messages.Length);

            var consecutiveSequenceRegex = new Regex("([a-zA-Z0-9]{2,})\\1");
            if (consecutiveSequenceRegex.IsMatch(password))
                result.Reasons.Add(Messages.IdenticalConsecutiveSequence);

            var alphaNumericRegex = new Regex(".*^[a-zA-Z0-9]+$");
            if (!alphaNumericRegex.IsMatch(password))
                result.Reasons.Add(Messages.AlphaNumeric);

            var containsAtLeastOneAlphaRegex = new Regex(".*[a-zA-Z]+.*");
            if (!containsAtLeastOneAlphaRegex.IsMatch(password))
                result.Reasons.Add(Messages.AtLeastOneAlpha);

            var containsAtLeastOneDigitRegex = new Regex(".*[0-9]+.*");
            if (!containsAtLeastOneDigitRegex.IsMatch(password))
                result.Reasons.Add(Messages.AtLeastOneDigit);

            return result;
        }

        public class Messages
        {
            public const string Length = "5-12 length";
            public const string IdenticalConsecutiveSequence = "no consecutive sequences";
            public const string AlphaNumeric = "must be alphanumeric";
            public const string AtLeastOneAlpha = "needs 1 letter";
            public const string AtLeastOneDigit = "needs 1 digit";
        }
    }
}
