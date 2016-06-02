using Catcher.MvvmCrossDemo.Core.ViewModels;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Catcher.MvvmCrossDemo.Second.Activities
{
    [Activity(Label ="Home",MainLauncher =true,LaunchMode =LaunchMode.SingleTop)]
    public class HomeActivity : MvxActivity
    {        
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.home);
        }
    }
}