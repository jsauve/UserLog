using MvvmCross.ViewModels;
using PropertyChanged;

namespace com.spectrum.UserLog.Core
{
    [AddINotifyPropertyChangedInterface]
    public class FirstViewModel : MvxViewModel
    {
        public string Hello { get; set; } = "Hello MvvmCross";
    }
}
