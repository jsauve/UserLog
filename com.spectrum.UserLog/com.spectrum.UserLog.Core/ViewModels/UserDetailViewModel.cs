using System;
using MvvmCross.ViewModels;
using PropertyChanged;

namespace com.spectrum.UserLog.Core
{
    public class UserDetailViewModel : BaseViewModel
    {
        public UserModel User { get; set; }

        public UserDetailViewModel()
        {
        }
    }
}
