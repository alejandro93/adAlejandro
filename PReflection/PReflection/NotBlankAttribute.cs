using System;

namespace PReflection
{
	public class NotBlankAttribute : ValidationAttribute
	{
		private string message = "El campo no debe estar vacío";
		public override string Validate (object value)
		{
			if (value == null || value.ToString ().Trim () == "")
				return message;
			return null;
		}
	}
}

