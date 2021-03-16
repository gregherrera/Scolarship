using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Escolaridad.Data;
using Escolaridad.Model;

namespace Escolaridad.Pages.Recintos
{
    public class DeleteModel : Escolaridad.Pages.PaginaAutenticada
    {
        private readonly Escolaridad.Data.ApplicationDbContext _context;

        public DeleteModel(Escolaridad.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recinto Recinto { get; set; }
        public Boolean Accion_Delete { get; set; }
        public Boolean Accion_Select { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recinto = await _context.Recintos.SingleOrDefaultAsync(m => m.Id == id);

            if (Recinto == null)
            {
                return NotFound();
            }

            Accion_Delete = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "D") == null ? false : true;
            Accion_Select = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "R") == null ? false : true;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recinto = await _context.Recintos.FindAsync(id);

            if (Recinto != null)
            {
                _context.Recintos.Remove(Recinto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
