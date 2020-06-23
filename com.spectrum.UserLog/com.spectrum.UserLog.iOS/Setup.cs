using com.spectrum.UserLog.Core;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.ViewModels;
using UIKit;

namespace com.spectrum.UserLog.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            var registry = Mvx.IoCProvider.Resolve<IMvxTargetBindingFactoryRegistry>();
            registry.RegisterFactory(new MvxCustomBindingFactory<UIViewController>("NetworkIndicator", (viewController) => new NetworkIndicatorTargetBinding(viewController)));
        }

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
