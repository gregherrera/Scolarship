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

namespace Escolaridad.Pages.Recintos
{
    [Authorize]
    public class EditModel : Escolaridad.Pages.PaginaAutenticada
    {
        private readonly Escolaridad.Data.ApplicationDbContext _context;

        public EditModel(Escolaridad.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recinto Recinto { get; set; }
        public Boolean Accion_Select { get; set; }
        public Boolean Accion_Update { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recinto = await _context.Recintos.Include(r => r.Barrio).SingleOrDefaultAsync(m => m.Id == id);

            if (Recinto == null)
            {
                return NotFound();
            }

            var _sec = await _context.Sectores.Include(s => s.Municipio).FirstOrDefaultAsync(s => s.Id == Recinto.Barrio.Id_Sector);
            var _mun = await _context.Municipios.Include(m => m.Provincia).FirstOrDefaultAsync(m => m.Id == _sec.Id_Municipio);
            var _prv = await _context.Provincias.FirstOrDefaultAsync(p => p.Id == _mun.Id_Provincia);

            ViewData["Provincias"] = new SelectList(_context.Provincias.Where(p => p.Id_Pais == this.GetPaisId()), "Id", "Descripcion", _prv.Id);
            ViewData["Municipios"] = new SelectList(_context.Municipios.Where(m => m.Id_Provincia == _prv.Id), "Id", "Descripcion", _mun.Id);
            ViewData["Sectores"] = new SelectList(_context.Sectores.Where(s => s.Id_Municipio == _mun.Id), "Id", "Descripcion", _sec.Id);
            ViewData["Barrios"] = new SelectList(_context.Barrios.Where(b => b.Id_Sector == _sec.Id), "Id", "Descripcion", id);

            Accion_Update = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == this.GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "U") == null ? false : true;
            Accion_Select = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == this.GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "R") == null ? false : true;

            return Page();
        }

        //Para llenar el dropdown de Municipios
        public JsonResult OnGetMunicipios(int Id_Provincia)
        {
            return new JsonResult(new SelectList(_context.Municipios.Where(p => p.Id_Provincia == Id_Provincia).ToList(), "Id", "Descripcion"));
        }

        //Para llenar el dropdown de Sectores
        public JsonResult OnGetSectores(int Id_Municipio)
        {
            return new JsonResult(new SelectList(_context.Sectores.Where(p => p.Id_Municipio == Id_Municipio).ToList(), "Id", "Descripcion"));
        }

        //Para llenar el dropdown de Barrios
        public JsonResult OnGetBarrios(int Id_Sector)
        {
            return new JsonResult(new SelectList(_context.Barrios.Where(p => p.Id_Sector == Id_Sector).ToList(), "Id", "Descripcion"));
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
                Recinto.Logo = ms.ToArray();
            }

            Recinto.Actualizado_Por = this.GetUserId();
            Recinto.Actualizado_En = DateTime.Now;

            _context.Attach(Recinto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecintoExists(Recinto.Id))
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

        private bool RecintoExists(int id)
        {
            return _context.Recintos.Any(e => e.Id == id);
        }
    }
}
