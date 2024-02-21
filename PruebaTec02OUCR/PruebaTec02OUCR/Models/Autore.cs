using System;
using System.Collections.Generic;

namespace PruebaTec02OUCR.Models
{
    public partial class Autore
    {
        public Autore()
        {
            Libros = new HashSet<Libro>();
        }

        public int IdAutor { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Libro> Libros { get; set; }
    }
}
