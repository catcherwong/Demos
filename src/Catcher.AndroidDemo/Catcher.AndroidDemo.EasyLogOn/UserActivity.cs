using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Catcher.AndroidDemo.EasyLogOn
{
    [Activity(Label = "用户首页")]
    public class UserActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.User);

            TextView info = FindViewById<TextView>(Resource.Id.tvInfo);

            string name = Intent.GetStringExtra("name");

            info.Text = name + "欢迎您的到来！" ;
        }
    }
}