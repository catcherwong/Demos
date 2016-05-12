using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Catcher.AndroidDemo.EasyLogOn
{
    [Activity(Label = "ListViewTestActivity",MainLauncher =true)]
    public class ListViewTestActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.mylistview);

            var lv = FindViewById<ListView>(Resource.Id.lv_list);

            IList<IDictionary<string, object>> dicList = new List<IDictionary<string, object>>();
            IDictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("MoneyId", Guid.Parse("5e611b36-fc26-489d-8a5f-59b1a5b10f8e"));
            dic.Add("CategoryName", "ÒûÊ³");
            //dic.Add("MoneyAbout", "10");
            //dic.Add("MoneyType", "Ö§³ö");
            //dic.Add("MoneyDate", "2016/4/30 0:00:00");
            dic.Add("MoneyValue", "10.00");

            string[] from = new string[] { "CategoryName", "MoneyValue" };
            int[] to = new int[] { Android.Resource.Id.Text1, Android.Resource.Id.Text2 };

            //List<Model> listt = new List<Model>();
            //listt.Add(new Model {  ID="1",Name="catcher"});
            //listt.Add(new Model { ID = "2", Name = "tom" });
            //listt.Add(new Model { ID = "3", Name = "wong" });
            //lv.Adapter = new SimpleAdapter(this, dicList, Android.Resource.Layout.SimpleListItem2, from, to);
            lv.Adapter = new MyAdapter(this,DB.Types);

        }
    }
}