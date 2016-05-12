using Android.App;
using Android.OS;

namespace Catcher.AndroidDemo.SplashDemo
{
    [Activity(Label = "Catcher.AndroidDemo.SplashDemo")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //set the view
            SetContentView (Resource.Layout.Main);
        }
    }
}

