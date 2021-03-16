using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Escolaridad.Model;

namespace Escolaridad.Data
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> Usr)
        {
            if (context.Users.FirstOrDefault(u => u.Email == "gregorio.herrera@gmail.com") == null)
            {
                var user = new ApplicationUser { Email = "gregorio.herrera@gmail.com", UserName = "gregorio.herrera@gmail.com", FirstName = "Gregorio U.", LastName = "Herrera Santos" };

                //El uso de este metodo es porque me encripta la contraseña a la manera de identity
                IdentityResult x = await Usr.CreateAsync(user, "Admin@123456");
            };

            var _user = context.Users.FirstOrDefault(u => u.Email == "gregorio.herrera@gmail.com");

            //Si no existe algun pais creado, procedo a crealo.
            if (!context.Paises.Any())
            {
                var pais = new Pais() { Descripcion = "República Dominicana", Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Paises.Add(pais);
                context.SaveChanges();
            }

            //Si no existe alguna provincia creada, procedo a creala.
            if (!context.Provincias.Any())
            {
                var provincia = new Provincia() { Descripcion = "Distrito Nacional", Id_Pais = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Provincias.Add(provincia);
                context.SaveChanges();
            }

            //Si no existe algun municipio creado, procedo a crealo.
            if (!context.Municipios.Any())
            {
                var municipio = new Municipio() { Descripcion = "Santo Domingo de Guzmán (Zona Urbana)", Id_Provincia = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Municipios.Add(municipio);
                context.SaveChanges();
            }

            //Si no existe algun sector creado, procedo a crealo.
            if (!context.Sectores.Any())
            {
                var sector = new Sector() { Descripcion = "Los Paralejos", Id_Municipio = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Sectores.Add(sector);
                context.SaveChanges();
            }

            //Si no existe algun barrio creado, procedo a crealo.
            if (!context.Barrios.Any())
            {
                var barrio = new Barrio() { Descripcion = "Los Paralejos", Id_Sector = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Barrios.Add(barrio);
                context.SaveChanges();
            }

            //Si no existe algun recinto creado, procedo a crealo.
            if (!context.Recintos.Any())
            {
                var recinto = new Recinto() { Descripcion = "Colegio Corderito de Jesús", Correo_Electronico="corderitodejesus@mail.com.do", Direccion="Calle primera #3", Lema="Instruye al niño en su camino y aun de viejo no se apartará de el.", Telefono="8092951135", Id_Barrio=1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Recintos.Add(recinto);
                context.SaveChanges();
            }

            //Si no existe el rol publico creado, procedo a crealo.
            if (context.Roles.FirstOrDefault(r => r.Name == "Publico") == null)
            {
                var role = new ApplicationRole { Name = "Publico" };
                context.Roles.Add(role);
                context.SaveChanges();
            }

            //Si no existe el rol administrador creado, procedo a crealo.
            if (context.Roles.FirstOrDefault(r => r.Name == "Administrador") == null)
            {
                var role = new ApplicationRole { Name = "Administrador" };
                context.Roles.Add(role);
                context.SaveChanges();
            }

            //Para crear la relacion usuario/role
            var _rol = context.Roles.FirstOrDefault(r => r.Name == "Administrador");

            var _usrRole = context.UserRoles.FirstOrDefault(r => r.UserId == _user.Id && r.RoleId == _rol.Id);
            if (_usrRole == null)
            {
                _usrRole = new IdentityUserRole<int> { UserId = _user.Id, RoleId = _rol.Id };
                context.UserRoles.Add(_usrRole);
                context.SaveChanges();
            }

            _rol = context.Roles.FirstOrDefault(r => r.Name == "Publico");

            _usrRole = context.UserRoles.FirstOrDefault(r => r.UserId == _user.Id && r.RoleId == _rol.Id);
            if (_usrRole == null)
            {
                _usrRole = new IdentityUserRole<int> { UserId = _user.Id, RoleId = _rol.Id };
                context.UserRoles.Add(_usrRole);
                context.SaveChanges();
            }

            //Para crear las acciones de las tablas Paises
            var _tab = context.AccionesTablas.FirstOrDefault(at => at.Tabla == "Paises" && at.Accion == "C");

            if (_tab == null)
            {
                _tab = new AccionesTabla() { Tabla = "Paises", Accion = "C", Creado_Por = _user.Id, Creado_En = System.DateTime.Now };
                context.AccionesTablas.Add(_tab);
                context.SaveChanges();
            }
            _tab = context.AccionesTablas.FirstOrDefault(at => at.Tabla == "Paises" && at.Accion == "R");
            if (_tab == null)
            {
                _tab = new AccionesTabla() { Tabla = "Paises", Accion = "R", Creado_Por = _user.Id, Creado_En = System.DateTime.Now };
                context.AccionesTablas.Add(_tab);
                context.SaveChanges();
            }
            _tab = context.AccionesTablas.FirstOrDefault(at => at.Tabla == "Paises" && at.Accion == "U");
            if (_tab == null)
            {
                _tab = new AccionesTabla() { Tabla = "Paises", Accion = "U", Creado_Por = _user.Id, Creado_En = System.DateTime.Now };
                context.AccionesTablas.Add(_tab);
                context.SaveChanges();
            }
            _tab = context.AccionesTablas.FirstOrDefault(at => at.Tabla == "Paises" && at.Accion == "D");
            if (_tab == null)
            {
                _tab = new AccionesTabla() { Tabla = "Paises", Accion = "D", Creado_Por = _user.Id, Creado_En = System.DateTime.Now };
                context.AccionesTablas.Add(_tab);
                context.SaveChanges();
            }

            //Para crear las acciones de las tablas Recintos
            _tab = context.AccionesTablas.FirstOrDefault(at => at.Tabla == "Recintos" && at.Accion == "C");
            if (_tab == null)
            {
                _tab = new AccionesTabla() { Tabla = "Recintos", Accion = "C", Creado_Por = _user.Id, Creado_En = System.DateTime.Now };
                context.AccionesTablas.Add(_tab);
                context.SaveChanges();
            }
            _tab = context.AccionesTablas.FirstOrDefault(at => at.Tabla == "Recintos" && at.Accion == "R");
            if (_tab == null)
            {
                _tab = new AccionesTabla() { Tabla = "Recintos", Accion = "R", Creado_Por = _user.Id, Creado_En = System.DateTime.Now };
                context.AccionesTablas.Add(_tab);
                context.SaveChanges();
            }
            _tab = context.AccionesTablas.FirstOrDefault(at => at.Tabla == "Recintos" && at.Accion == "U");
            if (_tab == null)
            {
                _tab = new AccionesTabla() { Tabla = "Recintos", Accion = "U", Creado_Por = _user.Id, Creado_En = System.DateTime.Now };
                context.AccionesTablas.Add(_tab);
                context.SaveChanges();
            }
            _tab = context.AccionesTablas.FirstOrDefault(at => at.Tabla == "Recintos" && at.Accion == "D");
            if (_tab == null)
            {
                _tab = new AccionesTabla() { Tabla = "Recintos", Accion = "D", Creado_Por = _user.Id, Creado_En = System.DateTime.Now };
                context.AccionesTablas.Add(_tab);
                context.SaveChanges();
            }

            //Si no existe algun Menu creado, procedo a crealo.
            if (!context.Menues.Any())
            {
                var menu = new Menu { Descripcion = "Administrativo", Nivel = 1, Orden = 1, URL = "/Index", Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Menues.Add(menu);

                menu = new Menu { Descripcion = "Docentes", Nivel = 1, Orden = 2, URL = "/Index", Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Menues.Add(menu);

                menu = new Menu { Descripcion = "Estudiantes", Nivel = 1, Orden = 3, URL = "/Index", Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Menues.Add(menu);

                menu = new Menu { Descripcion = "Paises", Nivel = 2, Orden = 1, URL = "/Paises", Id_Padre = 1, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Menues.Add(menu);

                menu = new Menu { Descripcion = "Recintos", Nivel = 2, Orden = 2, URL = "/Recintos", Id_Padre = 1, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Menues.Add(menu);

                menu = new Menu { Descripcion = "Inicio", Nivel = 1, Orden = 4, URL = "/Index", Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Menues.Add(menu);

                menu = new Menu { Descripcion = "Nosotros", Nivel = 1, Orden = 5, URL = "/About", Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Menues.Add(menu);

                menu = new Menu { Descripcion = "Contacto", Nivel = 1, Orden = 6, URL = "/Contact", Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.Menues.Add(menu);

                context.SaveChanges();
            }

            //Si no existe alguna relacion Rol-Menu creada, procedo a creala.
            if (!context.RoleMenues.Any())
            {
                var rolMenu = new RoleMenu { Id_Menu = 1, Id_Role = 2, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.RoleMenues.Add(rolMenu);

                rolMenu = new RoleMenu { Id_Menu = 2, Id_Role = 2, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.RoleMenues.Add(rolMenu);

                rolMenu = new RoleMenu { Id_Menu = 3, Id_Role = 2, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.RoleMenues.Add(rolMenu);

                rolMenu = new RoleMenu { Id_Menu = 4, Id_Role = 2, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.RoleMenues.Add(rolMenu);

                rolMenu = new RoleMenu { Id_Menu = 5, Id_Role = 2, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.RoleMenues.Add(rolMenu);

                rolMenu = new RoleMenu { Id_Menu = 6, Id_Role = 1, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.RoleMenues.Add(rolMenu);

                rolMenu = new RoleMenu { Id_Menu = 7, Id_Role = 1, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.RoleMenues.Add(rolMenu);

                rolMenu = new RoleMenu { Id_Menu = 8, Id_Role = 1, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.RoleMenues.Add(rolMenu);

                context.SaveChanges();
            }

            //Si no existe alguna relacion Usuario-Pais creada, procedo a creala.
            if (!context.UserPaises.Any())
            {
                var usrPais = new UserPais { Id_User = _user.Id, Id_Pais = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.UserPaises.Add(usrPais);

                context.SaveChanges();
            }

            //Si no existe alguna relacion Usuario-Recinto creada, procedo a creala.
            if (!context.UserRecintos.Any())
            {
                var usrRecinto = new UserRecinto { Id_User = _user.Id, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.UserRecintos.Add(usrRecinto);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion CREATE en la tabla PAISES, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == null && ua.Objeto.Tabla == "Paises" && ua.Objeto.Accion == "C") == null)
            {
                var usrAcciones = new UserAccionesTabla { 
                    Id_User = _user.Id, Id_Accion = 1, Id_Recinto = null, Creado_Por = _user.Id, Creado_En = DateTime.Now};
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion RETREAVE en la tabla PAISES, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == null && ua.Objeto.Tabla == "Paises" && ua.Objeto.Accion == "R") == null)
            {
                var usrAcciones = new UserAccionesTabla {
                    Id_User = _user.Id, Id_Accion = 2, Id_Recinto = null, Creado_Por = _user.Id, Creado_En = DateTime.Now};
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion UPDATE en la tabla PAISES, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == null && ua.Objeto.Tabla == "Paises" && ua.Objeto.Accion == "U") == null)
            {
                var usrAcciones = new UserAccionesTabla {
                    Id_User = _user.Id, Id_Accion = 3, Id_Recinto = null, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion DELETE en la tabla PAISES, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == null && ua.Objeto.Tabla == "Paises" && ua.Objeto.Accion == "D") == null)
            {
                var usrAcciones = new UserAccionesTabla {
                    Id_User = _user.Id, Id_Accion = 4, Id_Recinto = null, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion CREATE en la tabla PAISES, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == 1 && ua.Objeto.Tabla == "Paises" && ua.Objeto.Accion == "C") == null)
            {
                var usrAcciones = new UserAccionesTabla { 
                    Id_User = _user.Id, Id_Accion = 1, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now};
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion RETREAVE en la tabla PAISES, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == 1 && ua.Objeto.Tabla == "Paises" && ua.Objeto.Accion == "R") == null)
            {
                var usrAcciones = new UserAccionesTabla {
                    Id_User = _user.Id, Id_Accion = 2, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now};
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion UPDATE en la tabla PAISES, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == 1 && ua.Objeto.Tabla == "Paises" && ua.Objeto.Accion == "U") == null)
            {
                var usrAcciones = new UserAccionesTabla {
                    Id_User = _user.Id, Id_Accion = 3, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.UserAccionesTablas.Add(usrAcciones);
                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion DELETE en la tabla PAISES, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == 1 && ua.Objeto.Tabla == "Paises" && ua.Objeto.Accion == "D") == null)
            {
                var usrAcciones = new UserAccionesTabla {
                    Id_User = _user.Id, Id_Accion = 4, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion CREATE en la tabla RECINTOS, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == 1 && ua.Objeto.Tabla == "Recintos" && ua.Objeto.Accion == "C") == null)
            {
                var usrAcciones = new UserAccionesTabla { 
                    Id_User = _user.Id, Id_Accion = 5, Id_Recinto = null, Creado_Por = _user.Id, Creado_En = DateTime.Now};
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion RETREAVE en la tabla RECINTOS, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == 1 && ua.Objeto.Tabla == "Recintos" && ua.Objeto.Accion == "R") == null)
            {
                var usrAcciones = new UserAccionesTabla {
                    Id_User = _user.Id, Id_Accion = 6, Id_Recinto = null, Creado_Por = _user.Id, Creado_En = DateTime.Now};
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion UPDATE en la tabla RECINTOS, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == 1 && ua.Objeto.Tabla == "Recintos" && ua.Objeto.Accion == "U") == null)
            {
                var usrAcciones = new UserAccionesTabla {
                    Id_User = _user.Id, Id_Accion = 7, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.UserAccionesTablas.Add(usrAcciones);
                context.SaveChanges();
            }

            //Si no existe la relacion Usuario-Recinto-Accion-Tabla creada para la accion DELETE en la tabla RECINTOS, procedo a creala.
            if (context.UserAccionesTablas.Include(uat => uat.Objeto).SingleOrDefault(ua => ua.Id_User == _user.Id && ua.Id_Recinto == 1 && ua.Objeto.Tabla == "Recintos" && ua.Objeto.Accion == "D") == null)
            {
                var usrAcciones = new UserAccionesTabla {
                    Id_User = _user.Id, Id_Accion = 8, Id_Recinto = 1, Creado_Por = _user.Id, Creado_En = DateTime.Now };
                context.UserAccionesTablas.Add(usrAcciones);

                context.SaveChanges();
            }
        }
    }
}