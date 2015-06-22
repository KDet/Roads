package lvivroads.droidics.views;


public class DirectionView
	extends lvivroads.droidics.views.LocationView
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("LvivRoads.DroidICS.Views.DirectionView, LvivRoads.DroidICS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DirectionView.class, __md_methods);
	}


	public DirectionView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DirectionView.class)
			mono.android.TypeManager.Activate ("LvivRoads.DroidICS.Views.DirectionView, LvivRoads.DroidICS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
