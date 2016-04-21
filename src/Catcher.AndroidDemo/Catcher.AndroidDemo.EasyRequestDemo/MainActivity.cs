using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Catcher.AndroidDemo.Common;
using System.Json;

namespace Catcher.AndroidDemo.EasyRequestDemo
{
    [Activity(Label = "简单的网络请求Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {        
        EditText txtInput;
        Button btnPost;
        Button btnGet;
        TextView tv;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
          
            SetContentView(Resource.Layout.Main);

            txtInput = FindViewById<EditText>(Resource.Id.txt_input);
            btnPost = FindViewById<Button>(Resource.Id.btn_post);
            btnGet = FindViewById<Button>(Resource.Id.btn_get);
            tv = FindViewById<TextView>(Resource.Id.tv_result);

            btnPost.Click += PostRequest;
            btnGet.Click += GetRequest;
        }

        private async void PostRequest(object sender, EventArgs e)
        {
            string url = "http://192.168.1.102:8077/User/PostThing";
            IDictionary<string, string> routeParames = new Dictionary<string, string>();
            routeParames.Add("str", this.txtInput.Text);
            var data = await EasyWebRequest.DoPostAndGetRetuanValue(url, routeParames) as JsonObject;

            this.tv.Text = "hey," + data["Val"] + "!!i am from post";
        }

        private async void GetRequest(object sender, EventArgs e)
        {
            string url = "http://192.168.1.102:8077/User/GetThing";
            IDictionary<string, string> routeParames = new Dictionary<string, string>();
            routeParames.Add("str", this.txtInput.Text);
            var data = await EasyWebRequest.DoGetAndGetRetuanValue(url, routeParames) as JsonObject;

            this.tv.Text = "hey," + data["Val"] + "!!i am from get";
        }
    }
}

