using System;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private object id;
		public ArticuloView () : base(Gtk.WindowType.Toplevel)	{
			this.Build ();
		}

		public ArticuloView(object id) : this() {
			this.id = id;
			IDbCommand dbCommand = 
				App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = String.Format (
				"select * from categoria where id={0}", id
				);

			IDataReader dataReader = dbCommand.ExecuteReader ();
			dataReader.Read ();

			entryNombre.Text = dataReader ["nombre"].ToString ();
//			entryCategoria.Text = dataReader ["categoria"].ToString ();
//			entryPrecio.Text = dataReader ["precio"].ToString ();

			dataReader.Close ();
		}

		protected void OnSaveActionActivated (object sender, EventArgs e)
		{
			IDbCommand dbCommand = 
				App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = String.Format (
				"update categoria set nombre=@nombre where id={0}", id
				//falta anyadir update a los demás campos
				);

			DbCommandExtensions.AddParameter (dbCommand, "nombre", entryNombre.Text);
//			DbCommandExtensions.AddParameter (dbCommand, "categoria", entryCategoria.Text);
//			DbCommandExtensions.AddParameter (dbCommand, "precio", entryPrecio.Text);

			dbCommand.ExecuteNonQuery ();

			Destroy ();
		}
	}
}
