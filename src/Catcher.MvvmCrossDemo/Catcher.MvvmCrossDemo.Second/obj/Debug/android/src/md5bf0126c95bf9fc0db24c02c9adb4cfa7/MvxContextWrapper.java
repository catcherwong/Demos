package md5bf0126c95bf9fc0db24c02c9adb4cfa7;


public class MvxContextWrapper
	extends android.content.ContextWrapper
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getSystemService:(Ljava/lang/String;)Ljava/lang/Object;:GetGetSystemService_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("MvvmCross.Binding.Droid.Views.MvxContextWrapper, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", MvxContextWrapper.class, __md_methods);
	}


	public MvxContextWrapper (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == MvxContextWrapper.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxContextWrapper, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public java.lang.Object getSystemService (java.lang.String p0)
	{
		return n_getSystemService (p0);
	}

	private native java.lang.Object n_getSystemService (java.lang.String p0);

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
