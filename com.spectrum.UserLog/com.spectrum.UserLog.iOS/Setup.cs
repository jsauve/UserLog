using com.spectrum.UserLog.Core;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;

namespace com.spectrum.UserLog.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };
        }
    }
}
