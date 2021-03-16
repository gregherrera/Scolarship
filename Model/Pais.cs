using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using MySql.Data.EntityFrameworkCore.DataAnnotations;
using Escolaridad.Data;

namespace Escolaridad.Model
{
   //[MySqlCharset("utf8"), MySqlCollation("utf8_spanish_ci")]
    public class Pais
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name ="Nombre del País")]
        public string Descripcion { get; set; }
        
        //[Display(Name = "Bandera"), Column(TypeName = "MediumBlob")]
		[Display(Name = "Bandera")]
		public byte[] Bandera { get; set; }

        [Required]
        public int Creado_Por { get; set; }

        public DateTime Creado_En { get; set; }

        public int? Actualizado_Por { get; set; }

        public DateTime? Actualizado_En { get; set; }

        [ForeignKey("Creado_Por")]
        public ApplicationUser UsuarioCrea { get; set; }
        [ForeignKey("Actualizado_Por")]
        public ApplicationUser UsuarioModifica { get; set; }
        public ICollection<Provincia> Provincias { get; set; }
    }
}