using Android.App;
using Android.OS;

namespace GuideDemo
{
    [Activity(Label = "GuideDemo")]
    public class MainActivity : Activity
    {        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Main);         
        }
    }
}

