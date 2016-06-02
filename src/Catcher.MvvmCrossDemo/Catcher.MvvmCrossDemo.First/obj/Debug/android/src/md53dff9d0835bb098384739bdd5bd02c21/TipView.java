package md53dff9d0835bb098384739bdd5bd02c21;


public class TipView
	extends md5c293e307133ee8f46151deed2480c6a8.MvxActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Catcher.MvvmCrossDemo.First.Views.TipView, Catcher.MvvmCrossDemo.First, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TipView.class, __md_methods);
	}


	public TipView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TipView.class)
			mono.android.TypeManager.Activate ("Catcher.MvvmCrossDemo.First.Views.TipView, Catcher.MvvmCrossDemo.First, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
