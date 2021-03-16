using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Escolaridad.Data;
using Escolaridad.Pages;
using Escolaridad.Model;
using Escolaridad.Pages.Repository;

namespace WebApp
{
    //[Authorize]
    [SessionAlive]
    public class IndexModel : Escolaridad.Pages.PaginaAutenticada
    {
        //private IRepository<Pais> PaisRepositorio { get; set; }
        //private IRepository<UserAccionesTabla> AccionesUsrRepositorio { get; set; }

        //public IndexModel(IRepository<Pais> paisRepositorio, IRepository<UserAccionesTabla> accionesUsrRepositorio)
        //{
        //    PaisRepositorio = paisRepositorio;
        //    AccionesUsrRepositorio = accionesUsrRepositorio;
        //}

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IList<Pais> Pais { get;set; }
        public Boolean Accion_Select { get; set; }
        public Boolean Accion_Insert { get; set; }
        public Boolean Accion_Update { get; set; }
        public Boolean Accion_Delete { get; set; }

        public async Task OnGetAsync()
        {
            //Accion_Insert = await AccionesUsrRepositorio.GetAll(a => a.Id_User == 1 && a.Id_Recinto == 1 && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "C").SingleOrDefaultAsync() == null ? false : true;
            //Accion_Select = await AccionesUsrRepositorio.GetAll(a => a.Id_User == 1 && a.Id_Recinto == 1 && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "R").SingleOrDefaultAsync() == null ? false : true;
            //Accion_Update = await AccionesUsrRepositorio.GetAll(a => a.Id_User == 1 && a.Id_Recinto == 1 && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "U").SingleOrDefaultAsync() == null ? false : true;
            //Accion_Delete = await AccionesUsrRepositorio.GetAll(a => a.Id_User == 1 && a.Id_Recinto == 1 && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "D").SingleOrDefaultAsync() == null ? false : true;

            //Pais = await PaisRepositorio.GetAll().ToListAsync();

            Accion_Insert = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == this.GetUserId() && a.Id_Recinto == null && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "C") == null ? false : true;
            Accion_Select = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == this.GetUserId() && a.Id_Recinto == null && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "R") == null ? false : true;
            Accion_Update = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == this.GetUserId() && a.Id_Recinto == null && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "U") == null ? false : true;
            Accion_Delete = await _context.UserAccionesTablas.SingleOrDefaultAsync(a => a.Id_User == this.GetUserId() && a.Id_Recinto == null && a.Objeto.Tabla == "Paises" && a.Objeto.Accion == "D") == null ? false : true;

            Pais = await _context.Paises.ToListAsync();
        }
    }
}
