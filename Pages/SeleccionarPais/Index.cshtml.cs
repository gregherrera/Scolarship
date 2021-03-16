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

namespace Escolaridad.Pages.SeleccionarPais
{
    [Authorize]
    public class IndexModel : Escolaridad.Pages.PaginaAutenticada
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UserPais> Paises { get;set; }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            //Para obtener los pasises a los que tiene acceso el usuario
            Paises = await _context.UserPaises.Include(p => p.Pais).Where(u => u.Id_User == this.GetUserId()).ToListAsync();

            //Si solo tiene acceso a un pais, no mostramos la lista
            if (Paises.Count() == 1)
            {
                //Colocamos el pais en Session
                this.SetPaisId(Paises[0].Id_Pais);

                //Si hay una Url, redireccionamos hacia ella
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    
                    return RedirectToPage("/SeleccionarRecinto/Index", new { ReturnUrl = returnUrl });
                }
                else
                {
                    return LocalRedirect("/SeleccionarRecinto/Index");
                }
            }

            return Page();
        }

        public ActionResult OnPost(int id, string returnUrl = null)
        {
            //Colocamos el pais en Session
            this.SetPaisId(id);

            //Si hay una Url, redireccionamos hacia ella
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
    
            return RedirectToPage("/Index");
        }
    }
}