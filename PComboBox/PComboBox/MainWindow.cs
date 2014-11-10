using System;
using Gtk;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		List<Categoria> categorias = new List<Categoria> ();
		categorias.Add (new Categoria (1, "uno"));
		categorias.Add (new Categoria (2, "dos"));
		categorias.Add (new Categoria (3, "tres"));
		categorias.Add (new Categoria (4, "cuatro"));



		int idCategoria = 3;
//		CellRendererText cellRendererText = new CellRendererText ();
//		comboBox.PackStart (cellRendererText, false);
//		comboBox.AddAttribute (cellRendererText, "text", 0);
		CellRendererText cellRendererText2 = new CellRendererText ();
		comboBox.PackStart (cellRendererText2, false);
		comboBox.AddAttribute (cellRendererText2, "text", 1 );
		ListStore listStore = new ListStore (typeof(int), typeof(string));

		TreeIter treeIterZero = listStore.AppendValues (0, "<<Vacío>>");

		foreach (Categoria categoria in categorias) {
			TreeIter currentTreeIter = listStore.AppendValues (categoria.Id, categoria.Nombre);
			if (categoria.Id == idCategoria)
				treeIterZero = currentTreeIter;
		}
//		listStore.AppendValues (1, "Uno");
//		listStore.AppendValues (2, "Dos");

		comboBox.Model = listStore;

		comboBox.SetActiveIter (treeIterZero);

		TreeIter actualTreeIter;
		listStore.GetIterFirst (out actualTreeIter);
		listStore.GetValue (actualTreeIter, 0);


		propertiesAction.Activated += delegate {
			TreeIter treeIter;
			bool activaIter = comboBox.GetActiveIter(out treeIter);
			object id = activaIter ? listStore.GetValue (treeIter, 0) : 0;
			Console.WriteLine("id = {0}", id);
		};
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

public class Categoria{
	public Categoria (int id, string nombre){
		Id = id;
		Nombre = nombre;
	}
	public int Id { get; set; }
	public string Nombre { get; set; }

}
