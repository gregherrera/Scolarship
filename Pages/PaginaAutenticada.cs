using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Microsoft.AspNetCore.Http;

namespace Escolaridad.Pages
{
    public class PaginaAutenticada: PageModel
    {
        //Obtener el ID del usuario autenticado
        public Int32 GetUserId()
        {
            return Convert.ToInt32(this.User.FindFirst("UserId").Value);
        }

        //Guardar en session el pais seleccionado
        public void SetPaisId(Int32 _paisId)
        {
            HttpContext.Session.SetInt32("PaisID", _paisId);
        }

        //Obtener desde session el pais seleccionado
        public Int32? GetPaisId()
        {
            try
            {
                return HttpContext.Session.GetInt32("PaisID");
            }
            catch
            {
                return null;
            }
        }

        //Guardar en session el recinto seleccionado
        public void SetRecintoId(Int32 _recintoId)
        {
            HttpContext.Session.SetInt32("RecintoID", _recintoId);
        }

        //Obtener desde session el recinto seleccionado
        public Int32? GetRecintoId()
        {
            try
            {
                return HttpContext.Session.GetInt32("RecintoID");
            }
            catch
            {
                return null;
            }
        }

        //Borro toda la informacion en la session de la aplicación, esto incluye: usuario autenticado, pais y reciento seleccionado
        public void LimpiarSession()
        {
            HttpContext.Session.Clear();
        }
    }
}