using System;
using MvvmCross.ViewModels;
using PropertyChanged;

namespace com.spectrum.UserLog.Core
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel : MvxViewModel
    {
        public bool IsBusy { get; set; }
        public bool IsRefreshing { get; set; }

        protected BaseViewModel()
        {
        }
    }

    [AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult> where TParameter : class where TResult : class
    {
        public bool IsBusy { get; set; }
        public bool IsRefreshing { get; set; }

        protected BaseViewModel()
        {
        }
    }
}
