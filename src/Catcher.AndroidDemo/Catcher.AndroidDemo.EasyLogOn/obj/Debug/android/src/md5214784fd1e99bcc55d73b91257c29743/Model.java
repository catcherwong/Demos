package md5214784fd1e99bcc55d73b91257c29743;


public class Model
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Catcher.AndroidDemo.EasyLogOn.Model, Catcher.AndroidDemo.EasyLogOn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Model.class, __md_methods);
	}


	public Model () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Model.class)
			mono.android.TypeManager.Activate ("Catcher.AndroidDemo.EasyLogOn.Model, Catcher.AndroidDemo.EasyLogOn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
