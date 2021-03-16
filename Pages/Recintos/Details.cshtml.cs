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
    public class DetailsModel : Escolaridad.Pages.PaginaAutenticada
    {
        private readonly Escolaridad.Data.ApplicationDbContext _context;

        public DetailsModel(Escolaridad.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Recinto Recinto { get; set; }
        public Boolean Accion_Select { get; set; }
        public string Usuario_Crea { get; set; }
        public string Usuario_Modifica { get; set; }
        public string Provincia { get; set; }
        public string Municipio { get; set; }
        public string Sector { get; set; }
        public string Barrio { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recinto = await _context.Recintos
                .Include(r => r.Barrio)
                .Include(r => r.UsuarioCrea)
                .Include(r => r.UsuarioModifica).SingleOrDefaultAsync(m => m.Id == id);

            if (Recinto == null)
            {
                return NotFound();
            }

            Usuario_Crea = Recinto.UsuarioCrea.FirstName + " " + Recinto.UsuarioCrea.LastName;

            if (Recinto.UsuarioModifica == null)
            {
                Usuario_Modifica = "";
            }
            else
            {
                Usuario_Modifica = Recinto.UsuarioModifica.FirstName + " " + Recinto.UsuarioModifica.LastName;
            }

            var _sec = await _context.Sectores.Include(s => s.Municipio).FirstOrDefaultAsync(s => s.Id == Recinto.Barrio.Id_Sector);
            var _mun = await _context.Municipios.Include(m => m.Provincia).FirstOrDefaultAsync(m => m.Id == _sec.Id_Municipio);
            var _prv = await _context.Provincias.FirstOrDefaultAsync(p => p.Id == _mun.Id_Provincia);

            Provincia = _context.Provincias.FirstOrDefault(p => p.Id == _prv.Id).Descripcion;
            Municipio = _context.Municipios.FirstOrDefault(m => m.Id == _mun.Id).Descripcion;
            Sector = _context.Sectores.FirstOrDefault(s => s.Id == _sec.Id).Descripcion;
            Barrio = _context.Barrios.FirstOrDefault(b => b.Id == Recinto.Barrio.Id).Descripcion;

            Accion_Select = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "R") == null ? false : true;

            return Page();
        }
    }
}
