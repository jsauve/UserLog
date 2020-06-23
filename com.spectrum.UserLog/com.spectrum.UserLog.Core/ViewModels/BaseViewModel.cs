using MvvmCross.ViewModels;
using PropertyChanged;

namespace com.spectrum.UserLog.Core
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel : MvxViewModel
    {
    }

    [AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel<TParameter> : MvxViewModel<TParameter> where TParameter : BaseModel
    {
    }
}
