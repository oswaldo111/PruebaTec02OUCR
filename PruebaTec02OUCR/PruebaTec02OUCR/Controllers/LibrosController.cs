using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTec02OUCR.Models;

namespace PruebaTec02OUCR.Controllers
{
    public class LibrosController : Controller
    {
        private readonly EditorialContext _context;

        public LibrosController(EditorialContext context)
        {
            _context = context;
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            var editorialContext = _context.Libros.Include(l => l.IdAutorNavigation);
            return View(await editorialContext.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.IdAutorNavigation)
                .FirstOrDefaultAsync(m => m.LibrosId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Nombre");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibrosId,Nombre,Precio,Descripcion,Imagen,IdAutor")] Libro libro, IFormFile imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    libro.Imagen = memoryStream.ToArray();
                }
            }
            _context.Add(libro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Nombre", libro.IdAutor);
            return View(libro);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibrosId,Nombre,Precio,Descripcion,Imagen,IdAutor")] Libro libro, IFormFile imagen)
        {
            if (id != libro.LibrosId)
            {
                return NotFound();
            }

            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    libro.Imagen = memoryStream.ToArray();
                }
                _context.Update(libro);
                await _context.SaveChangesAsync();
            }
            else
            {
                var librosFind = await _context.Libros.FirstOrDefaultAsync(s => s.LibrosId == libro.LibrosId);
                if (librosFind?.Imagen?.Length > 0)
                    libro.Imagen = librosFind.Imagen;
                librosFind.Nombre = libro.Nombre;
                librosFind.Precio = libro.Precio;
                librosFind.Descripcion = libro.Descripcion;
                librosFind.IdAutor = libro.IdAutor;
                _context.Update(librosFind);
                await _context.SaveChangesAsync();
            }
            try
            {

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(libro.LibrosId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.IdAutorNavigation)
                .FirstOrDefaultAsync(m => m.LibrosId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Libros == null)
            {
                return Problem("Entity set 'EditorialContext.Libros'  is null.");
            }
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteImagen(int? id)
        {
            var librosFind = await _context.Libros.FirstOrDefaultAsync(s => s.LibrosId == id);
            librosFind.Imagen = null;
            _context.Update(librosFind);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
          return (_context.Libros?.Any(e => e.LibrosId == id)).GetValueOrDefault();
        }
    }
}
