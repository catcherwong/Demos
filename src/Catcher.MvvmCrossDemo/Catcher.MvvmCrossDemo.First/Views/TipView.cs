using Android.App;
using MvvmCross.Droid.Views;

namespace Catcher.MvvmCrossDemo.First.Views
{
    [Activity(Label ="Test" , MainLauncher =true)]
    public class TipView : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Main);
        }        
    }
}