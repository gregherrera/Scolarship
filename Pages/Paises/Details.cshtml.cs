using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Escolaridad.Data;
using Escolaridad.Model;

namespace Escolaridad
{
    public class DetailsModel : Escolaridad.Pages.PaginaAutenticada
    {
        private readonly Escolaridad.Data.ApplicationDbContext _context;

        public DetailsModel(Escolaridad.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Pais Pais { get; set; }
        public Boolean Accion_Select { get; set; }
        public string Usuario_Crea { get; set; }
        public string Usuario_Modifica { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pais = await _context.Paises
                .Include(p => p.UsuarioCrea)
                .Include(p => p.UsuarioModifica).SingleOrDefaultAsync(m => m.Id == id);

            if (Pais == null)
            {
                return NotFound();
            }

            Usuario_Crea = Pais.UsuarioCrea.FirstName + " " + Pais.UsuarioCrea.LastName;

            if (Pais.UsuarioModifica == null)
            {
                Usuario_Modifica = "";
            }
            else
            {
                Usuario_Modifica = Pais.UsuarioModifica.FirstName + " " + Pais.UsuarioModifica.LastName;
            }

            Accion_Select = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == GetUserId() && a.Id_Recinto == null && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "R") == null ? false : true;

            return Page();
        }
    }
}
