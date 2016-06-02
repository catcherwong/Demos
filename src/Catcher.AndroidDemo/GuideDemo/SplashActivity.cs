using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace GuideDemo
{
    [Activity(Label = "GuideDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.splash);
            //version's infomation
            var version = PackageManager.GetPackageInfo(this.PackageName, PackageInfoFlags.MatchAll).VersionName;

            var tvVersion = FindViewById<TextView>(Resource.Id.tv_version);
            tvVersion.Text = "Version " + version;
            //get infomation from shared preferences
            var sp = GetSharedPreferences("app_setting", FileCreationMode.Private);

            new Handler().PostDelayed(() =>
            {
                Intent intent;
                //first or not
                if (sp.GetString("version", "") != version)
                {
                    intent = new Intent(this, typeof(GuideActivity));
                }
                else
                {
                    intent = new Intent(this, typeof(MainActivity));
                }                
                StartActivity(intent);
                this.Finish();
            }, 1000);
        }
    }
}