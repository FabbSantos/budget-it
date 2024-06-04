using System;

namespace WebApplication.Models
{
    public class Usuario
    {
        public string Name { get; set; }

        public Usuario() // Add a parameterless constructor
        {
        }

        // Existing constructor with parameters
        public Usuario(string nome)
        {
            Name = nome;
        }
    }

}