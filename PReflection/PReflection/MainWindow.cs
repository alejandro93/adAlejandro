using System;
using Gtk;
using System.Reflection;
using PReflection;


public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
//		showInfo (typeof(Categoria));
//		showInfo (typeof(Articulo));
//		showInfo (typeof(Button));
////		Type type = typeof(MainWindow);
//		Assembly assembly = Assembly.GetExecutingAssembly ();
//		foreach (Type type in assembly.GetTypes())
//			if (type.IsDefined (typeof(EntityAttribute), true)) {
//				EntityAttribute entityAttribute =
//					(EntityAttribute)Attribute.GetCustomAttribute (type, typeof(EntityAttribute));
//				Console.WriteLine ("type.Name={0} entityAttribute.TableName={1}", 
//			                   type.Name, entityAttribute.TableName);
////				Console.WriteLine ("type.Name={0}", type.Name);
//			}
		Categoria categoria = new Categoria (33, "");
		Validate (categoria);
		categoria.Nombre = "algo";
		Validate (categoria);
//		showValues (categoria);



	}

	private void showValues (object obj) {
		Type type = obj.GetType ();
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
		foreach (FieldInfo field in fields) {
			object value = field.GetValue (obj);
			Console.WriteLine ("field.Name={0, -30} value={1}", field.Name, value);
		}

	}
	private void Validate(object obj){
		ErrorInfo[] errorInfo = Validator.Validate (obj);
		if (errorInfo.Length == 0)
			Console.WriteLine ("Sin errores");
		foreach (ErrorInfo errorInfos in errorInfo) {
			Console.WriteLine ("property={0} message={1}", errorInfos.Property, errorInfos.Message);
		}
	}
	private void showInfo(Type type){
		Console.WriteLine("type.Name{0}", type.Name);
		PropertyInfo[] properties = type.GetProperties();

		foreach (PropertyInfo property in properties) 
			Console.WriteLine("property.Name={0, -20} property.PropertyType={1}", property.Name, property.PropertyType);

		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
		foreach (FieldInfo field in fields) {
//			if (field.IsDefined(typeof(IdAttribute), true))
			Console.WriteLine ("field.Name={0, -30} field.FieldType={1}", field.Name, field.FieldType);
		}
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
