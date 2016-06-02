package md5c293e307133ee8f46151deed2480c6a8;


public abstract class MvxSplashScreenActivity
	extends md5c293e307133ee8f46151deed2480c6a8.MvxActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"";
		mono.android.Runtime.register ("MvvmCross.Droid.Views.MvxSplashScreenActivity, MvvmCross.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", MvxSplashScreenActivity.class, __md_methods);
	}


	public MvxSplashScreenActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MvxSplashScreenActivity.class)
			mono.android.TypeManager.Activate ("MvvmCross.Droid.Views.MvxSplashScreenActivity, MvvmCross.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
