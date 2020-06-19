using System;
using PropertyChanged;

namespace com.spectrum.UserLog.Core
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
    }
}
