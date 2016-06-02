package md5bf0126c95bf9fc0db24c02c9adb4cfa7;


public class MvxLayoutInflater
	extends android.view.LayoutInflater
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_cloneInContext:(Landroid/content/Context;)Landroid/view/LayoutInflater;:GetCloneInContext_Landroid_content_Context_Handler\n" +
			"n_inflate:(ILandroid/view/ViewGroup;Z)Landroid/view/View;:GetInflate_ILandroid_view_ViewGroup_ZHandler\n" +
			"n_onCreateView:(Landroid/view/View;Ljava/lang/String;Landroid/util/AttributeSet;)Landroid/view/View;:GetOnCreateView_Landroid_view_View_Ljava_lang_String_Landroid_util_AttributeSet_Handler\n" +
			"n_onCreateView:(Ljava/lang/String;Landroid/util/AttributeSet;)Landroid/view/View;:GetOnCreateView_Ljava_lang_String_Landroid_util_AttributeSet_Handler\n" +
			"n_setFactory:(Landroid/view/LayoutInflater$Factory;)V:__export__\n" +
			"n_setFactory2:(Landroid/view/LayoutInflater$Factory2;)V:__export__\n" +
			"";
		mono.android.Runtime.register ("MvvmCross.Binding.Droid.Views.MvxLayoutInflater, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", MvxLayoutInflater.class, __md_methods);
	}


	public MvxLayoutInflater (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == MvxLayoutInflater.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxLayoutInflater, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public MvxLayoutInflater (android.view.LayoutInflater p0, android.content.Context p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == MvxLayoutInflater.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxLayoutInflater, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.LayoutInflater, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public android.view.LayoutInflater cloneInContext (android.content.Context p0)
	{
		return n_cloneInContext (p0);
	}

	private native android.view.LayoutInflater n_cloneInContext (android.content.Context p0);


	public android.view.View inflate (int p0, android.view.ViewGroup p1, boolean p2)
	{
		return n_inflate (p0, p1, p2);
	}

	private native android.view.View n_inflate (int p0, android.view.ViewGroup p1, boolean p2);


	public android.view.View onCreateView (android.view.View p0, java.lang.String p1, android.util.AttributeSet p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.View p0, java.lang.String p1, android.util.AttributeSet p2);


	public android.view.View onCreateView (java.lang.String p0, android.util.AttributeSet p1)
	{
		return n_onCreateView (p0, p1);
	}

	private native android.view.View n_onCreateView (java.lang.String p0, android.util.AttributeSet p1);


	public void setFactory (android.view.LayoutInflater.Factory p0)
	{
		n_setFactory (p0);
	}

	private native void n_setFactory (android.view.LayoutInflater.Factory p0);


	public void setFactory2 (android.view.LayoutInflater.Factory2 p0)
	{
		n_setFactory2 (p0);
	}

	private native void n_setFactory2 (android.view.LayoutInflater.Factory2 p0);

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
