using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PruebaTec02OUCR.Models
{
    public partial class EditorialContext : DbContext
    {
        public EditorialContext()
        {
        }

        public EditorialContext(DbContextOptions<EditorialContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autore> Autores { get; set; } = null!;
        public virtual DbSet<Libro> Libros { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=NOSE345\\UNICA;Initial Catalog=Editorial;Integrated Security=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autore>(entity =>
            {
                entity.HasKey(e => e.IdAutor)
                    .HasName("PK__Autores__DD33B03163DAF511");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.HasKey(e => e.LibrosId)
                    .HasName("PK__Libros__F2DF8B9CD22EC04A");

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.Libros)
                    .HasForeignKey(d => d.IdAutor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Libros__IdAutor__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
