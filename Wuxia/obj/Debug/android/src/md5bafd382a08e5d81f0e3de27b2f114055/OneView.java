package md5bafd382a08e5d81f0e3de27b2f114055;


public class OneView
	extends android.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Wuxia.Fragments.OneView, Wuxia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", OneView.class, __md_methods);
	}


	public OneView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == OneView.class)
			mono.android.TypeManager.Activate ("Wuxia.Fragments.OneView, Wuxia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
