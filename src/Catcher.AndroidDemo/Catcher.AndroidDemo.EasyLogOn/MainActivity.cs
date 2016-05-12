using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Catcher.AndroidDemo.EasyLogOn
{
    [Activity(Label = "简单的登录Demo", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            EditText myName = FindViewById<EditText>(Resource.Id.txtName);
            EditText myPwd = FindViewById<EditText>(Resource.Id.txtPwd);
            Button login = FindViewById<Button>(Resource.Id.btnLogin);

            login.Click += delegate
           {
               string name = myName.Text;
               string pwd = myPwd.Text;

               if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
               {
                   Toast.MakeText(this, "请输入用户名和密码！！", ToastLength.Long).Show();
                   return;
               }
               else
               {
                   string loginUrl = string.Format("http://192.168.1.102:8077/User/LogOn?userName={0}&userPwd={1}", name, pwd);

                   var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(loginUrl));
                   var httpRes = (HttpWebResponse)httpReq.GetResponse();
                   if (httpRes.StatusCode == HttpStatusCode.OK)
                   {
                       string result = new StreamReader(httpRes.GetResponseStream()).ReadToEnd();

                       result = result.Replace("\"", "'");

                       ReturnModel s = JsonConvert.DeserializeObject<ReturnModel>(result);

                       if (s.Code == "00000")
                       {
                           var intent = new Intent(this, typeof(UserActivity));

                           intent.PutExtra("name", name);

                           StartActivity(intent);
                       }
                       else
                       {
                           Toast.MakeText(this, "用户名或密码不正确！！", ToastLength.Long).Show();
                           return;
                       }
                   }
               }
           };
        }
    }

    public class ReturnModel
    {
        public string Code { get; set; }
        public string Msg { get; set; }
    }
}
