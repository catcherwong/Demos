using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Catcher.AndroidDemo.LoadingAnimationDemo
{
    [Activity(Label = "LastActivity")]
    public class LastActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.last);
            TextView tvShow = FindViewById<TextView>(Resource.Id.tv_show);
            tvShow.Text = Intent.GetStringExtra("name");
        }
    }
}