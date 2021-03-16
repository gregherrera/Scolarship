using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Escolaridad.Data;
using System;

namespace Escolaridad.Pages
{
    public class SessionAliveAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Session == null) //No hay variable de Session
            {
                context.Result = new RedirectResult("/Account/Login");
                return;
            }
            else if (!context.HttpContext.User.Identity.IsAuthenticated) //No usuario no autenticado
            {
                context.Result = new RedirectResult("/Account/Login");
                return;
            }
            else if (!context.HttpContext.Session.GetInt32("PaisID").HasValue) //No se ha seleccionado un pais
            {
                context.Result = new RedirectResult("/SeleccionarPais?ReturnUrl=" + context.HttpContext.Request.Path.Value);
                return;
            }
            else if (!context.HttpContext.Session.GetInt32("RecintoID").HasValue) //No se ha seleccionado un recinto
            {
                context.Result = new RedirectResult("/SeleccionarRecinto?ReturnUrl=" + context.HttpContext.Request.Path.Value);
                return;
            }

            base.OnResultExecuting(context);
        }
    }
}