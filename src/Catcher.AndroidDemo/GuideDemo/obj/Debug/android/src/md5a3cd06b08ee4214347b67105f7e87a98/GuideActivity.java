package md5a3cd06b08ee4214347b67105f7e87a98;


public class GuideActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("GuideDemo.GuideActivity, GuideDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GuideActivity.class, __md_methods);
	}


	public GuideActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GuideActivity.class)
			mono.android.TypeManager.Activate ("GuideDemo.GuideActivity, GuideDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
