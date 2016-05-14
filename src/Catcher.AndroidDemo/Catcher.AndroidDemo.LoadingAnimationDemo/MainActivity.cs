using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Catcher.AndroidDemo.LoadingAnimationDemo.Extensions;
using System.Threading.Tasks;

namespace Catcher.AndroidDemo.LoadingAnimationDemo
{
    [Activity(Label = "Catcher.AndroidDemo.LoadingAnimationDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        CustomProgressDialog dialog;

        protected override void OnCreate(Bundle bundle)
        {            
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            //create a new dialog
            dialog = CustomProgressDialog.CreateDialog(this);
            dialog.OnWindowFocusChanged(true);
                        
            Button btnGO = FindViewById<Button>(Resource.Id.go);
            btnGO.Click += (s,e) => 
            {
                int result = 0;
                //show the dialog
                dialog.Show();
                //do some things
                Task task = new Task(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        result += i;
                    }
                });
                task.ContinueWith(t =>
                {
                    Intent intent = new Intent(this, typeof(LastActivity));
                    intent.PutExtra("name", result.ToString());  
                    StartActivity(intent);
                });
                task.Start();                                
            };           
        }      

        protected override void OnResume()
        {
            base.OnResume();
            if(dialog.IsShowing)
            { 
                dialog.Dismiss();
            }
        }
    }
}