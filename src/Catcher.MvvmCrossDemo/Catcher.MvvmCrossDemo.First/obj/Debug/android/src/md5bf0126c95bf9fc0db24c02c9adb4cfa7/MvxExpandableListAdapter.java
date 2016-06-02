package md5bf0126c95bf9fc0db24c02c9adb4cfa7;


public class MvxExpandableListAdapter
	extends md5bf0126c95bf9fc0db24c02c9adb4cfa7.MvxAdapter
	implements
		mono.android.IGCUserPeer,
		android.widget.ExpandableListAdapter
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getGroupCount:()I:GetGetGroupCountHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_hasStableIds:()Z:GetHasStableIdsHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_isEmpty:()Z:GetIsEmptyHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_areAllItemsEnabled:()Z:GetAreAllItemsEnabledHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getChild:(II)Ljava/lang/Object;:GetGetChild_IIHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getChildId:(II)J:GetGetChildId_IIHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getChildrenCount:(I)I:GetGetChildrenCount_IHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getChildView:(IIZLandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetChildView_IIZLandroid_view_View_Landroid_view_ViewGroup_Handler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getCombinedChildId:(JJ)J:GetGetCombinedChildId_JJHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getCombinedGroupId:(J)J:GetGetCombinedGroupId_JHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getGroup:(I)Ljava/lang/Object;:GetGetGroup_IHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getGroupId:(I)J:GetGetGroupId_IHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getGroupView:(IZLandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetGroupView_IZLandroid_view_View_Landroid_view_ViewGroup_Handler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_isChildSelectable:(II)Z:GetIsChildSelectable_IIHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onGroupCollapsed:(I)V:GetOnGroupCollapsed_IHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onGroupExpanded:(I)V:GetOnGroupExpanded_IHandler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_registerDataSetObserver:(Landroid/database/DataSetObserver;)V:GetRegisterDataSetObserver_Landroid_database_DataSetObserver_Handler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_unregisterDataSetObserver:(Landroid/database/DataSetObserver;)V:GetUnregisterDataSetObserver_Landroid_database_DataSetObserver_Handler:Android.Widget.IExpandableListAdapterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("MvvmCross.Binding.Droid.Views.MvxExpandableListAdapter, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", MvxExpandableListAdapter.class, __md_methods);
	}


	public MvxExpandableListAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MvxExpandableListAdapter.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxExpandableListAdapter, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MvxExpandableListAdapter (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MvxExpandableListAdapter.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxExpandableListAdapter, MvvmCross.Binding.Droid, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public int getGroupCount ()
	{
		return n_getGroupCount ();
	}

	private native int n_getGroupCount ();


	public boolean hasStableIds ()
	{
		return n_hasStableIds ();
	}

	private native boolean n_hasStableIds ();


	public boolean isEmpty ()
	{
		return n_isEmpty ();
	}

	private native boolean n_isEmpty ();


	public boolean areAllItemsEnabled ()
	{
		return n_areAllItemsEnabled ();
	}

	private native boolean n_areAllItemsEnabled ();


	public java.lang.Object getChild (int p0, int p1)
	{
		return n_getChild (p0, p1);
	}

	private native java.lang.Object n_getChild (int p0, int p1);


	public long getChildId (int p0, int p1)
	{
		return n_getChildId (p0, p1);
	}

	private native long n_getChildId (int p0, int p1);


	public int getChildrenCount (int p0)
	{
		return n_getChildrenCount (p0);
	}

	private native int n_getChildrenCount (int p0);


	public android.view.View getChildView (int p0, int p1, boolean p2, android.view.View p3, android.view.ViewGroup p4)
	{
		return n_getChildView (p0, p1, p2, p3, p4);
	}

	private native android.view.View n_getChildView (int p0, int p1, boolean p2, android.view.View p3, android.view.ViewGroup p4);


	public long getCombinedChildId (long p0, long p1)
	{
		return n_getCombinedChildId (p0, p1);
	}

	private native long n_getCombinedChildId (long p0, long p1);


	public long getCombinedGroupId (long p0)
	{
		return n_getCombinedGroupId (p0);
	}

	private native long n_getCombinedGroupId (long p0);


	public java.lang.Object getGroup (int p0)
	{
		return n_getGroup (p0);
	}

	private native java.lang.Object n_getGroup (int p0);


	public long getGroupId (int p0)
	{
		return n_getGroupId (p0);
	}

	private native long n_getGroupId (int p0);


	public android.view.View getGroupView (int p0, boolean p1, android.view.View p2, android.view.ViewGroup p3)
	{
		return n_getGroupView (p0, p1, p2, p3);
	}

	private native android.view.View n_getGroupView (int p0, boolean p1, android.view.View p2, android.view.ViewGroup p3);


	public boolean isChildSelectable (int p0, int p1)
	{
		return n_isChildSelectable (p0, p1);
	}

	private native boolean n_isChildSelectable (int p0, int p1);


	public void onGroupCollapsed (int p0)
	{
		n_onGroupCollapsed (p0);
	}

	private native void n_onGroupCollapsed (int p0);


	public void onGroupExpanded (int p0)
	{
		n_onGroupExpanded (p0);
	}

	private native void n_onGroupExpanded (int p0);


	public void registerDataSetObserver (android.database.DataSetObserver p0)
	{
		n_registerDataSetObserver (p0);
	}

	private native void n_registerDataSetObserver (android.database.DataSetObserver p0);


	public void unregisterDataSetObserver (android.database.DataSetObserver p0)
	{
		n_unregisterDataSetObserver (p0);
	}

	private native void n_unregisterDataSetObserver (android.database.DataSetObserver p0);

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
