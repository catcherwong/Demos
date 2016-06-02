using Android.Content;
using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using Catcher.MvvmCrossDemo.Core;

namespace Catcher.MvvmCrossDemo.Second
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
    }
}