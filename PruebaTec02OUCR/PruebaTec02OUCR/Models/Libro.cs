using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTec02OUCR.Models
{
    public partial class Libro
    {
        public int LibrosId { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }

        
        public int IdAutor { get; set; }
        [Display (Name = "Autor")]
        public virtual Autore IdAutorNavigation { get; set; } = null!;
    }
}
