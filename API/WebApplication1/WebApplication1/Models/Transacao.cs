using System;

namespace WebApplication1.Controllers
{
	public class Transacao
	{
		public int Id_transacao {  get; set; }

		public int Id_usuario { get; set; }

		public long Valor { get; set; }

		public string ?Tipo { get; set; }

		public string ?Date {  get; set; }

	}
}
