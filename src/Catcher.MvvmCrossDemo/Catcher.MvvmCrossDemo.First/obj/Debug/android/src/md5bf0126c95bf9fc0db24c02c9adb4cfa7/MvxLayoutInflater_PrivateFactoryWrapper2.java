package md5bf0126c95bf9fc0db24c02c9adb4cfa7;


public class MvxLayoutInflater_PrivateFactoryWrapper2
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.LayoutInflater.Factory2,
		android.view.LayoutInflater.Factory
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateView:(Landroid/view/View;Ljava/lang/String;Landroid/content/Context;Landroid/util/AttributeSet;)Landroid/view/View;:GetOnCreateView_Landroid_view_View_Ljava_lang_String_Landroid_content_Context_Landroid_util_AttributeSet_Handler:Android.Views.LayoutInflater/IFactory2Invoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onCreateView:(Ljava/lang/String;Landroid/content/Context;Landroid/util/AttributeSet;)Landroid/view/View;:GetOnCreateView_Ljava_lang_String_Landroid_content_Context_Landroid_util_AttributeSet_Handler:Android.Views.LayoutInflater/IFactoryInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("MvvmCross.Binding.Droid.Views.MvxLayoutInflater+PrivateFactoryWrapper2, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", MvxLayoutInflater_PrivateFactoryWrapper2.class, __md_methods);
	}


	public MvxLayoutInflater_PrivateFactoryWrapper2 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MvxLayoutInflater_PrivateFactoryWrapper2.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxLayoutInflater+PrivateFactoryWrapper2, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.view.View onCreateView (android.view.View p0, java.lang.String p1, android.content.Context p2, android.util.AttributeSet p3)
	{
		return n_onCreateView (p0, p1, p2, p3);
	}

	private native android.view.View n_onCreateView (android.view.View p0, java.lang.String p1, android.content.Context p2, android.util.AttributeSet p3);


	public android.view.View onCreateView (java.lang.String p0, android.content.Context p1, android.util.AttributeSet p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (java.lang.String p0, android.content.Context p1, android.util.AttributeSet p2);

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
