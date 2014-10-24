using System;

namespace PCategoria
{
	public partial class App : Gtk.ActionGroup
	{
		public App () : 
				base("PCategoria.App")
		{
			this.Build ();
		}
	}
}

