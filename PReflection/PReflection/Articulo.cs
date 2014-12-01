using System;

namespace PReflection
{
	[Entity]
	public class Articulo
	{
		public ulong Id{ get; set;}
		public string Nombre { get; set;}
		public float Precio { get; set; }
		public Categoria Categoria {get; set;}
	}
}

