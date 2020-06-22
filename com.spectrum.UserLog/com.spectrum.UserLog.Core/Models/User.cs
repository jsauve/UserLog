using System;
using Newtonsoft.Json;

namespace com.spectrum.UserLog.Core
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        [JsonIgnore]
        public string DisplayName => $"{LastName}, {FirstName}";
    }
}
