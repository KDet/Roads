package lvivroads.droidics.views;


public class LocationView
	extends lvivroads.droidics.views.BaseView
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("LvivRoads.DroidICS.Views.LocationView, LvivRoads.DroidICS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LocationView.class, __md_methods);
	}


	public LocationView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LocationView.class)
			mono.android.TypeManager.Activate ("LvivRoads.DroidICS.Views.LocationView, LvivRoads.DroidICS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
