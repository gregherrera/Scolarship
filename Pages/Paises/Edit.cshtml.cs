using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Escolaridad.Data;
using Escolaridad.Model;

namespace WebApp
{
    [Authorize]
    public class EditModel : Escolaridad.Pages.PaginaAutenticada
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pais Pais { get; set; }
        public Boolean Accion_Update { get; set; }
        public Boolean Accion_Select { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Contiene la data del pais que deseamos modificar
            Pais = await _context.Paises.SingleOrDefaultAsync(m => m.Id == id);

            if (Pais == null)
            {
                return NotFound();
            }

            Accion_Update = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == this.GetUserId() && a.Id_Recinto == null && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "U") == null ? false : true;
            Accion_Select = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == this.GetUserId() && a.Id_Recinto == null && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "R") == null ? false : true;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (file != null)
            {
                MemoryStream ms = new MemoryStream();
                file.OpenReadStream().CopyTo(ms);
                Pais.Bandera = ms.ToArray();
            }

            Pais.Actualizado_Por = this.GetUserId();
            Pais.Actualizado_En = DateTime.Now;

            _context.Attach(Pais).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaisExists(Pais.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PaisExists(int id)
        {
            return _context.Paises.Any(e => e.Id == id);
        }
    }
}
