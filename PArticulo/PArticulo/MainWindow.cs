using System;
using Gtk;
using SerpisAd;
using System.Data;
using MySql.Data.MySqlClient;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	private ListStore listStore;
	private ListStore listStore2;
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		dbConnection = App.Instance.DbConnection;

		deleteAction.Sensitive = false;
		editAction.Sensitive = false;
		deleteAction1.Sensitive = false;
		editAction1.Sensitive = false;

		treeview1.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeview1.AppendColumn ("artículo", new CellRendererText (), "text", 1);
		treeview1.AppendColumn ("categoría", new CellRendererText (), "text", 2);
		treeview1.AppendColumn ("precio", new CellRendererText (), "text", 3);

		listStore = new ListStore (typeof(ulong), typeof(string), typeof(string), typeof(string));
		treeview1.Model = listStore;

		fillListStore ();
		treeview1.Selection.Changed += selectionChanged;

		treeview2.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeview2.AppendColumn ("categoría", new CellRendererText (), "text", 1);
		treeview2.AppendColumn ("artículo", new CellRendererText (), "text", 2);

		listStore2 = new ListStore (typeof(ulong),typeof(string), typeof(string));
		treeview2.Model = listStore2;

		fillListStore2 ();
		treeview2.Selection.Changed += selectionChanged2;
	}
	private void selectionChanged (object sender, EventArgs e) {
		Console.WriteLine ("selectionChanged");
		bool hasSelected = treeview1.Selection.CountSelectedRows () > 0;
		deleteAction.Sensitive = hasSelected;
		editAction.Sensitive = hasSelected;
	}
	private void selectionChanged2 (object sender, EventArgs e) {
		Console.WriteLine ("selectionChanged");
		bool hasSelected = treeview2.Selection.CountSelectedRows () > 0;
		deleteAction1.Sensitive = hasSelected;
		editAction1.Sensitive = hasSelected;
	}

	private void fillListStore() {
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from categoria";

		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];
			object cat = dataReader ["cat"];
			object precio = dataReader ["precio"].ToString();
			listStore.AppendValues (id, nombre, cat, precio);
		}
		dataReader.Close ();
	}

	private void fillListStore2() {
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from categoria";

		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object cat = dataReader ["cat"];
			object nombre = dataReader["nombre"];
			listStore2.AppendValues (id, cat, nombre);
		}
		dataReader.Close();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		dbConnection.Close ();
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnQuitActionActivated (object sender, EventArgs e)
	{
		dbConnection.Close ();
		Application.Quit ();
	}
	
	protected void OnDeleteActionActivated (object sender, EventArgs e)
	{
		MessageDialog messageDialog = new MessageDialog (
			this,
			DialogFlags.Modal,
			MessageType.Question,
			ButtonsType.YesNo,
			"¿Quieres eliminar el registro?"
			);
		messageDialog.Title = Title;
		ResponseType response = (ResponseType) messageDialog.Run ();
		messageDialog.Destroy ();

		if (response != ResponseType.Yes)
			return;

		TreeIter treeIter;
		treeview1.Selection.GetSelected (out treeIter);
		object id = listStore.GetValue (treeIter, 0);
		string deleteSql = string.Format ("delete from categoria where id={0}", id);
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = deleteSql;

		dbCommand.ExecuteNonQuery ();
	}
	protected void OnDeleteAction1Activated (object sender, EventArgs e)
	{
		MessageDialog messageDialog = new MessageDialog (
			this,
			DialogFlags.Modal,
			MessageType.Question,
			ButtonsType.YesNo,
			"¿Quieres eliminar el registro?"
			);
		messageDialog.Title = Title;
		ResponseType response = (ResponseType) messageDialog.Run ();
		messageDialog.Destroy ();

		if (response != ResponseType.Yes)
			return;

		TreeIter treeIter;
		treeview2.Selection.GetSelected (out treeIter);
		object id = listStore2.GetValue (treeIter, 0);
		string deleteSql = string.Format ("delete from categoria where id={0}", id);
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = deleteSql;

		dbCommand.ExecuteNonQuery ();
	}
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		listStore.Clear ();
		fillListStore ();
	}

	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		string insertSql = string.Format(
			"insert into categoria (nombre) values ('{0}')",
			"Nuevo " + DateTime.Now
			);
		Console.WriteLine ("insertSql={0}", insertSql);
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = insertSql;

		dbCommand.ExecuteNonQuery ();
	}

	protected void OnEditActionActivated (object sender, EventArgs e)
	{
		TreeIter treeIter;
		treeview1.Selection.GetSelected (out treeIter);
		object id = listStore.GetValue (treeIter, 0);
		ArticuloView articuloView = new ArticuloView (id);
	}



	protected void OnAddAction1Activated (object sender, EventArgs e)
	{
		string insertSql = string.Format(
			"insert into categoria (nombre) values ('{0}')",
			"Nuevo " + DateTime.Now
			);
		Console.WriteLine ("insertSql={0}", insertSql);
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = insertSql;

		dbCommand.ExecuteNonQuery ();
	}

	protected void OnRefreshAction1Activated (object sender, EventArgs e)
	{
		listStore2.Clear ();
		fillListStore2 ();
	}

	protected void OnEditAction1Activated (object sender, EventArgs e)
	{
		TreeIter treeIter;
		treeview2.Selection.GetSelected (out treeIter);
		object id = listStore2.GetValue (treeIter, 0);
		ArticuloView articuloView2 = new ArticuloView (id);
	}
}
