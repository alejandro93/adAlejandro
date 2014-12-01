using System;
using System.Collections.Generic;
using System.Reflection;

namespace PReflection
{
	public static class Validator
	{
		public static ErrorInfo[] Validate (object obj){
			List <ErrorInfo> list = new List <ErrorInfo> ();
			Type type = obj.GetType ();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (FieldInfo field in fields) 
				if (field.IsDefined (typeof(ValidationAttribute), true)) {
					ValidationAttribute validationAttribute =
					(ValidationAttribute)Attribute.GetCustomAttribute (type, typeof(ValidationAttribute),true);
					object value = field.GetValue (obj);
					string message = validationAttribute.Validate (value);
					if (message != null) {
						list.Add (new ErrorInfo (field.Name, message));
					}
				}
			return list.ToArray ();
		}
	}
}

