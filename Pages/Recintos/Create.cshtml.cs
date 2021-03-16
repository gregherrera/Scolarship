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
using Escolaridad.Data;
using Escolaridad.Model;

namespace WebApp.Pages.Recintos
{
    //[Authorize]
    public class CreateModel : Escolaridad.Pages.PaginaAutenticada
    {
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Accion_Insert = _context.UserAccionesTablas.SingleOrDefault(a => a.Id_User == this.GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "C") == null ? false : true;
            Accion_Select = _context.UserAccionesTablas.SingleOrDefault(a => a.Id_User == this.GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "R") == null ? false : true;

            return Page();
        }

        [BindProperty]
        public Recinto Recinto { get; set; }

        private readonly ApplicationDbContext _context;
        public Boolean Accion_Insert { get; set; }
        public Boolean Accion_Select { get; set; }

        //Para llenar el dropdown de Provincias
        public JsonResult OnGetProvincias()
        {
            return new JsonResult(new SelectList(_context.Provincias.Where(p => p.Id_Pais == this.GetPaisId()).ToList(), "Id", "Descripcion"));
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

            Recinto.Creado_Por = this.GetUserId();
            Recinto.Creado_En = DateTime.Now;

            _context.Recintos.Add(Recinto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}