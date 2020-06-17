using System;
namespace com.spectrum.UserLog.Core
{
    public class UserModel : BaseModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName => $"{LastName}, {FirstName}";
    }
}
