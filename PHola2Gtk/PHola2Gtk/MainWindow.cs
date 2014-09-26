using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButtonClicked (object sender, EventArgs e)
	{
		Console.WriteLine ("Has hecho clic");
		labelSaludo.Text = "Hola" + entry.Text;
	}

	protected void OnButton3Clicked (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}
	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		Console.WriteLine ("Has activado la acción newAction");
	}
	
}
