using System;
using MySql.Data.MySqlClient;

namespace PHolaMySql
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			MySqlConnection mySqlConnection = new MySqlConnection (
				"Server=localhost;" +
				"Database=dbprueba;" +
				"User ID=root;" +
				"Password=sistemas");

			mySqlConnection.Open ();


			Console.WriteLine ("Hola MySql!");

//			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
//			mySqlCommand.CommandText =
//				string.Format ("insert into categoria (nombre) values ('{0}')",
//			                DateTime.Now);
//
//			mySqlCommand.ExecuteNonQuery ();

			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();

			mySqlCommand.CommandText = "select * from categoria";

			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

			Console.WriteLine ("FieldCoun={0}", mySqlDataReader.FieldCount);
			for (int index = 0; index < mySqlDataReader.FieldCount; index++)
				Console.WriteLine ("column {0}={1}", index, mySqlDataReader.GetName (index));
			

			mySqlConnection.Close ();

		}
	}
}
