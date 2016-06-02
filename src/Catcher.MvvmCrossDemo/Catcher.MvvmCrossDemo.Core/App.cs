using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Catcher.MvvmCrossDemo.Core.Services;
using Catcher.MvvmCrossDemo.Core.ViewModels;

namespace Catcher.MvvmCrossDemo.Core
{
    public class App : MvxApplication
    {
        public App()
        {

            //first
            //Mvx.RegisterType<ICalculation, Calculation>();
            //Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<TipViewModel>());   
            //second
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<SecondHomeViewModel>());
        }

    }
}
