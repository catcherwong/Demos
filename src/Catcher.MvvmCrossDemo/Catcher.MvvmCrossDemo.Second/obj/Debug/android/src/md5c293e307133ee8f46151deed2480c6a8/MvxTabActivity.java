package md5c293e307133ee8f46151deed2480c6a8;


public abstract class MvxTabActivity
	extends md5f60cb25cf67aec87462671c9e2811eb9.MvxEventSourceTabActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_setContentView:(I)V:GetSetContentView_IHandler\n" +
			"n_attachBaseContext:(Landroid/content/Context;)V:GetAttachBaseContext_Landroid_content_Context_Handler\n" +
			"";
		mono.android.Runtime.register ("MvvmCross.Droid.Views.MvxTabActivity, MvvmCross.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", MvxTabActivity.class, __md_methods);
	}


	public MvxTabActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MvxTabActivity.class)
			mono.android.TypeManager.Activate ("MvvmCross.Droid.Views.MvxTabActivity, MvvmCross.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void setContentView (int p0)
	{
		n_setContentView (p0);
	}

	private native void n_setContentView (int p0);


	public void attachBaseContext (android.content.Context p0)
	{
		n_attachBaseContext (p0);
	}

	private native void n_attachBaseContext (android.content.Context p0);

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
