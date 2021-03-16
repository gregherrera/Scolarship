using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Escolaridad.Model;
//using MySql.Data.EntityFrameworkCore.Extensions;

namespace Escolaridad.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(e => { e.ToTable(name: "Users"); });
            builder.Entity<ApplicationRole>(e => { e.ToTable(name: "Roles"); });
            builder.Entity<IdentityRoleClaim<Int32>>(e => { e.ToTable(name: "RoleClaims"); });
            builder.Entity<IdentityUserClaim<Int32>>(e => { e.ToTable(name: "UserClaims"); });
            builder.Entity<IdentityUserLogin<Int32>>(e => { e.ToTable(name: "UserLogins"); });
            builder.Entity<IdentityUserRole<Int32>>(e => { e.ToTable(name: "UserRoles"); });
            builder.Entity<IdentityUserToken<Int32>>(e => { e.ToTable(name: "UserTokens"); });
            
            builder.Entity<RoleMenu>().HasIndex(rm => new { rm.Id_Recinto, rm.Id_Role, rm.Id_Menu }).IsUnique(true);
            builder.Entity<UserPais>().HasIndex(up => new { up.Id_User, up.Id_Pais }).IsUnique(true);
            builder.Entity<UserRecinto>().HasIndex(ur => new { ur.Id_User, ur.Id_Recinto }).IsUnique(true);
            builder.Entity<AccionesTabla>().HasIndex(at => new { at.Tabla, at.Accion }).IsUnique(true);
            builder.Entity<UserAccionesTabla>().HasIndex(uat => new { uat.Id_User, uat.Id_Recinto, uat.Id_Accion }).IsUnique(true);

			builder.Entity<AccionesTabla>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<AccionesTabla>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Barrio>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Barrio>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Menu>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Menu>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Menu>().HasOne<Menu>(m => m.MenuPadre).WithMany(m => m.Menues).OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Menu>().HasOne<Recinto>(r => r.Recinto).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Municipio>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Municipio>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Municipio>().HasOne<Provincia>(p => p.Provincia).WithMany(p => p.Municipios).OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Pais>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Pais>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Provincia>().HasOne<Pais>(p => p.Pais).WithMany(p => p.Provincias).OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Provincia>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Provincia>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Recinto>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Recinto>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Recinto>().HasOne<Barrio>(b => b.Barrio).WithOne().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<RoleMenu>().HasOne<Recinto>(r => r.Recinto).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<RoleMenu>().HasOne<Menu>(m => m.Menu).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<RoleMenu>().HasOne<ApplicationRole>(ar => ar.Role).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<RoleMenu>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<RoleMenu>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Sector>().HasOne<Municipio>(m => m.Municipio).WithMany(s => s.Sectores).OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Sector>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<Sector>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<UserAccionesTabla>().HasOne<Recinto>(r => r.Recinto).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserAccionesTabla>().HasOne<AccionesTabla>(at => at.Objeto).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserAccionesTabla>().HasOne<ApplicationUser>(au => au.Usuario).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserAccionesTabla>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserAccionesTabla>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<UserPais>().HasOne<Pais>(p => p.Pais).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserPais>().HasOne<ApplicationUser>(au => au.Usuario).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserPais>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserPais>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Entity<UserRecinto>().HasOne<Recinto>(r => r.Recinto).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserRecinto>().HasOne<ApplicationUser>(au => au.Usuario).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserRecinto>().HasOne<ApplicationUser>(au => au.UsuarioCrea).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Entity<UserRecinto>().HasOne<ApplicationUser>(au => au.UsuarioModifica).WithMany().OnDelete(DeleteBehavior.NoAction);
		}

		public virtual DbSet<Pais> Paises { get; set; }
        public virtual DbSet<Provincia> Provincias { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<Sector> Sectores { get; set; }
        public virtual DbSet<Barrio> Barrios { get; set; }
        public virtual DbSet<Recinto> Recintos { get; set; }
        public virtual DbSet<Menu> Menues { get; set; }
        public virtual DbSet<RoleMenu> RoleMenues { get; set; }
        public virtual DbSet<UserPais> UserPaises { get; set; }
        public virtual DbSet<UserRecinto> UserRecintos { get; set; }
        public virtual DbSet<AccionesTabla> AccionesTablas { get; set; }
        public virtual DbSet<UserAccionesTabla> UserAccionesTablas { get; set; }
    }
}
