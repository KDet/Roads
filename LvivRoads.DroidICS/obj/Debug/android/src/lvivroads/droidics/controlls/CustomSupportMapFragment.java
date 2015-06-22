package lvivroads.droidics.controlls;


public class CustomSupportMapFragment
	extends com.google.android.gms.maps.SupportMapFragment
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("LvivRoads.DroidICS.Controlls.CustomSupportMapFragment, LvivRoads.DroidICS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomSupportMapFragment.class, __md_methods);
	}


	public CustomSupportMapFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CustomSupportMapFragment.class)
			mono.android.TypeManager.Activate ("LvivRoads.DroidICS.Controlls.CustomSupportMapFragment, LvivRoads.DroidICS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
