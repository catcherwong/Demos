using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;

namespace Catcher.AndroidDemo.SplashDemo
{
    [Activity(Label = "SplashActivity", MainLauncher = true,NoHistory =true ,Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.splash);
            //version's infomation
            var tvVersion = FindViewById<TextView>(Resource.Id.tv_version);    
            tvVersion.Text ="Version "+ PackageManager.GetPackageInfo(this.PackageName,PackageInfoFlags.MatchAll).VersionName;

            //Method 1：
            //用过java写Android的应该比较熟悉
            new Handler().PostDelayed(() =>
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                this.Finish();
            }, 5000);

            //Method 2:
            //这种写法只是休眠5秒然后就把这个页面闪现一下就跳转到主页面了
            //Thread.Sleep(5000);
            //this.StartActivity(typeof(MainActivity));
            //this.Finish();

            //Method 3:
            //这种写法改进了第二种写法的出现的问题
            //Thread thread =  new Thread(() => 
            //{
            //    Thread.Sleep(5000);                
            //    Intent intent = new Intent(this, typeof(MainActivity));
            //    StartActivity(intent);
            //    this.Finish();
            //});            
            //thread.Start();

            //Method 4:
            //用Task来实现
            //Task task = new Task(() =>
            //{
            //    Task.Delay(5000);            
            //});
            //task.ContinueWith(t =>
            //{
            //    StartActivity(new Intent(this, typeof(MainActivity)));
            //    this.Finish();
            //},TaskScheduler.FromCurrentSynchronizationContext());
            //task.Start();


            //Method 5:
            //new Handler().PostDelayed(new Java.Lang.Runnable(() =>
            //{
            //    Intent intent = new Intent(this, typeof(MainActivity));
            //    StartActivity(intent);
            //    this.Finish();
            //}), 5000);

        }
    }
}