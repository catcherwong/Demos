using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

namespace Catcher.AndroidDemo.LoadingAnimationDemo.Extensions
{
    public class CustomProgressDialog : Dialog
    {        
        private static CustomProgressDialog customProgressDialog;
        
        public CustomProgressDialog(Context context):base(context)
        {                        
        }

        public CustomProgressDialog(Context context, int themeResId):base(context,themeResId)
        {        
        }

        /// <summary>
        /// create the dialog
        /// </summary>
        /// <param name="context">the context</param>
        /// <returns>the instance of the customize dialog</returns>
        public static CustomProgressDialog CreateDialog(Context context)
        {
            customProgressDialog = new CustomProgressDialog(context, Resource.Style.CustomProgressDialog);
            //set the view
            customProgressDialog.SetContentView(Resource.Layout.loading);
            //set the gravity
            customProgressDialog.Window.Attributes.Gravity = GravityFlags.Center;

            return customProgressDialog;
        }

        /// <summary>
        /// called whenever the window focus changes
        /// </summary>
        /// <param name="hasFocus">whether the window now has focus</param>
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);

            if (customProgressDialog == null)
            {
                return;
            }

            ImageView imageView = customProgressDialog.FindViewById<ImageView>(Resource.Id.loadingImageView);            
            AnimationDrawable animationDrawable = (AnimationDrawable)imageView.Background;
            //start the animation
            animationDrawable.Start();
        }        
    }
}