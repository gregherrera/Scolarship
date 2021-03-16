using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Escolaridad.Data;
using Escolaridad.Model;

namespace Escolaridad.Pages.Recintos
{
    [Authorize]
    public class IndexModel : Escolaridad.Pages.PaginaAutenticada
    {
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;
        public Boolean Accion_Select { get; set; }
        public Boolean Accion_Insert { get; set; }
        public Boolean Accion_Update { get; set; }
        public Boolean Accion_Delete { get; set; }
        public IList<Recinto> Recinto { get;set; }

        public async Task OnGetAsync()
        {
            Accion_Insert = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "C") == null ? false : true;
            Accion_Select = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "R") == null ? false : true;
            Accion_Update = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "U") == null ? false : true;
            Accion_Delete = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == GetUserId() && a.Id_Recinto == GetRecintoId() && a.Objeto.Tabla == "Recintos" && a.Objeto.Accion == "D") == null ? false : true;

            Recinto = await _context.Recintos.Include(r => r.Barrio).ToListAsync();
        }
    }
}
