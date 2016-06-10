using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using static Android.Support.V4.View.ViewPager;

namespace GuideDemo
{
    [Activity(Label = "GuideActivity")] 
    public class GuideActivity : Activity
    {     
        private ViewPager viewPager;
     
        private List<View> views;
     
        private View view1, view2, view3;
     
        private Button btnStart;
            
        private ImageView[] dots;
        
        private int currentIndex;

        private LinearLayout ll;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_guide);

            viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            //the layout
            LayoutInflater mLi = LayoutInflater.From(this);
            view1 = mLi.Inflate(Resource.Layout.guide_first, null);
            view2 = mLi.Inflate(Resource.Layout.guide_second, null);
            view3 = mLi.Inflate(Resource.Layout.guide_third, null);

            views = new List<View>();

            views.Add(view1);
            views.Add(view2);
            views.Add(view3);
            
            //the adapter
            viewPager.Adapter = new ViewPagerAdapter(views);

            //page selected
            viewPager.PageSelected += PageSelected;

            ll = FindViewById<LinearLayout>(Resource.Id.ll);

            //the dot infomation
            dots = new ImageView[3];
            for (int i = 0; i < views.Count; i++)
            {
                dots[i] = (ImageView)ll.GetChildAt(i);
                dots[i].Enabled = false;                
            }
            dots[0].Enabled = true;

            //the button
            btnStart = view3.FindViewById<Button>(Resource.Id.startBtn);
            btnStart.Click += Start;
        }

        /// <summary>
        /// page selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageSelected(object sender, PageSelectedEventArgs e)
        {
            ll = FindViewById<LinearLayout>(Resource.Id.ll);
            for (int i = 0; i < views.Count; i++)
            {
                dots[i] = (ImageView)ll.GetChildAt(i);
                dots[i].Enabled = false;
            }
            dots[e.Position].Enabled = true;
        }

        /// <summary>
        /// start the main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start(object sender, EventArgs e)
        {
            //get infomation from shared preferences
            var sp = GetSharedPreferences("app_setting", FileCreationMode.Private);
            //the editor
            var editor = sp.Edit();
            //update
            editor.PutString("version", PackageManager.GetPackageInfo(this.PackageName, PackageInfoFlags.MatchAll).VersionName).Commit() ;
            StartActivity(typeof(MainActivity));
            this.Finish();
        }
    }
}