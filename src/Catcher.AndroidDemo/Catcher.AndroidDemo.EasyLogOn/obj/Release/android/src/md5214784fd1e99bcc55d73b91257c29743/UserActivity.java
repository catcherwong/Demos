package md5214784fd1e99bcc55d73b91257c29743;


public class UserActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Catcher.AndroidDemo.EasyLogOn.UserActivity, Catcher.AndroidDemo.EasyLogOn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", UserActivity.class, __md_methods);
	}


	public UserActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == UserActivity.class)
			mono.android.TypeManager.Activate ("Catcher.AndroidDemo.EasyLogOn.UserActivity, Catcher.AndroidDemo.EasyLogOn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
