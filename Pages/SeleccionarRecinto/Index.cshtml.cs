using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Escolaridad.Data;
using Escolaridad.Model;

namespace Escolaridad.Pages.SeleccionarRecinto
{
    [Authorize]
    public class IndexModel : Escolaridad.Pages.PaginaAutenticada
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UserRecinto> Recintos { get;set; }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            //Para obtener los recintos a los que tiene acceso el usuario para el pais seleccionado
            Recintos = await _context.UserRecintos.Include(r => r.Recinto).Where(ur => ur.Id_User == this.GetUserId() && ur.Recinto.Barrio.Sector.Municipio.Provincia.Id_Pais == this.GetPaisId()).ToListAsync();

            //Si solo tiene acceso a un recinto en el pais seleccionado, no mostramos la lista
            if (Recintos.Count() == 1)
            {
                //Colocamos el recinto en Session
                this.SetRecintoId(Recintos[0].Id_Recinto);

                //Si hay una Url, redireccionamos hacia ella
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return RedirectToPage(Url.GetLocalUrl(ReturnUrl));
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }

        public ActionResult OnPost(int id, string returnUrl = null)
        {
            //Colocamos el recinto en Session
            this.SetRecintoId(id);

            //Si hay una Url, redireccionamos hacia ella
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
    
            return RedirectToPage("/Index");
        }
    }
}