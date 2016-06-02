using Android.Support.V4.View;
using Android.Views;
using System.Collections.Generic;

namespace GuideDemo
{
    internal class ViewPagerAdapter : PagerAdapter
    {
        private List<View> views;

        public ViewPagerAdapter(List<View> views)
        {
            this.views = views;
        }

        public override int Count
        {
            get
            {
                if (views != null)
                {
                    return views.Count;
                }
                else
                { 
                    return 0;
                }
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view== objectValue;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue)
        {
            container.RemoveView(views[position]);
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            container.AddView(views[position], 0);
            return views[position];
        }
    }
}