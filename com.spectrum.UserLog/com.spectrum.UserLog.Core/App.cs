using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace com.spectrum.UserLog.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton(() => UserDialogs.Instance);

            RegisterAppStart<UsersViewModel>();
        }
    }
}
