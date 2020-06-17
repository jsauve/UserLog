using System;
using MvvmCross.ViewModels;

namespace com.spectrum.UserLog.Core
{
    public abstract class BaseCollectionViewModel<TModel> : BaseViewModel where TModel : BaseModel
    {
        public MvxObservableCollection<TModel> Collection { get; private set; } = new MvxObservableCollection<TModel>();
    }
}
