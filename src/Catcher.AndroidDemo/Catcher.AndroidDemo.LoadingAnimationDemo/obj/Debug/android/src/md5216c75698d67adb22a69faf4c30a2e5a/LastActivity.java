package md5216c75698d67adb22a69faf4c30a2e5a;


public class LastActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Catcher.AndroidDemo.LoadingAnimationDemo.LastActivity, Catcher.AndroidDemo.LoadingAnimationDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LastActivity.class, __md_methods);
	}


	public LastActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LastActivity.class)
			mono.android.TypeManager.Activate ("Catcher.AndroidDemo.LoadingAnimationDemo.LastActivity, Catcher.AndroidDemo.LoadingAnimationDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
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
