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
        Button btnPostHWR;
        Button btnGetHWR;
        TextView tv;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
          
            SetContentView(Resource.Layout.Main);

            txtInput = FindViewById<EditText>(Resource.Id.txt_input);
            btnPost = FindViewById<Button>(Resource.Id.btn_post);
            btnGet = FindViewById<Button>(Resource.Id.btn_get);
            btnGetHWR = FindViewById<Button>(Resource.Id.btn_getHWR);
            btnPostHWR = FindViewById<Button>(Resource.Id.btn_postHWR);
            tv = FindViewById<TextView>(Resource.Id.tv_result);

            //based on httpclient
            btnPost.Click += PostRequest;
            btnGet.Click += GetRequest;
            //based on httpwebrequest
            btnPostHWR.Click += PostRequestByHWR;
            btnGetHWR.Click += GetRequestByHWR;
        }

        private async void GetRequestByHWR(object sender, EventArgs e)
        {
            string url = "http://192.168.1.102:8077/User/GetThing";
            IDictionary<string, string> routeParames = new Dictionary<string, string>();
            routeParames.Add("str", this.txtInput.Text);
            var result = await EasyWebRequest.SendGetHttpRequestBaseOnHttpWebRequest(url, routeParames);
            var data = (JsonObject)result;
            this.tv.Text = "hey," + data["Val"] + ",  i am from httpwebrequest get";
        }

        private async void PostRequestByHWR(object sender, EventArgs e)
        {
            string url = "http://192.168.1.102:8077/User/PostThing";
            IDictionary<string, string> routeParames = new Dictionary<string, string>();
            routeParames.Add("str", this.txtInput.Text);
            var result = await EasyWebRequest.SendPostHttpRequestBaseOnHttpWebRequest(url, routeParames);
            var data = (JsonObject)result;
            this.tv.Text = "hey," + data["Val"] + ",  i am from httpwebrequest post";
        }

        private async void PostRequest(object sender, EventArgs e)
        {
            string url = "http://192.168.1.102:8077/User/PostThing";
            IDictionary<string, string> routeParames = new Dictionary<string, string>();
            routeParames.Add("str", this.txtInput.Text);
            var result = await EasyWebRequest.SendPostRequestBasedOnHttpClient(url, routeParames);
            var data = (JsonObject)result;
            this.tv.Text = "hey," + data["Val"] + ",  i am from httpclient post";
        }

        private async void GetRequest(object sender, EventArgs e)
        {
            string url = "http://192.168.1.102:8077/User/GetThing";
            IDictionary<string, string> routeParames = new Dictionary<string, string>();
            routeParames.Add("str", this.txtInput.Text);
            var result = await EasyWebRequest.SendGetRequestBasedOnHttpClient(url, routeParames);
            var data = (JsonObject)result;
            this.tv.Text = "hey," + data["Val"] + ",  i am from httpclient get";
        }
    }
}

