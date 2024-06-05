using System;

namespace WebApplication.Models
{
    public class Usuario
    {
        public int? Id_usuario { get; set;} 
        public string Nome { get; set; }
        public long? Saldo { get; set; }

        public Usuario() // Add a parameterless constructor
        {
        }

        // Existing constructor with parameters
        public Usuario(string nome, long saldo)
        {
            Nome = nome;
            Saldo = saldo;
        }
    }

}