using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Escolaridad.Data;
using Escolaridad.Model;

namespace Escolaridad
{
    public class CreateModel : Escolaridad.Pages.PaginaAutenticada
    {
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Accion_Insert = _context.UserAccionesTablas.SingleOrDefault(a => a.Id_User == this.GetUserId() && a.Id_Recinto == null && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "C") == null ? false : true;
            Accion_Select = _context.UserAccionesTablas.SingleOrDefault(a => a.Id_User == this.GetUserId() && a.Id_Recinto == null && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "R") == null ? false : true;

            return Page();
        }

        [BindProperty]
        public Pais Pais { get; set; }

        private readonly ApplicationDbContext _context;
        public Boolean Accion_Insert { get; set; }
        public Boolean Accion_Select { get; set; }

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

            Pais.Creado_Por = this.GetUserId();
            Pais.Creado_En = DateTime.Now;

            _context.Paises.Add(Pais);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}